using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SharkBehavior : MonoBehaviour
{
    public Vector2 PatrolPoint;
    public float Speed;

    private Vector2 _startPosition;
    private bool _outbound;
    private Vector2 _attractTarget;

	// Use this for initialization
	void Start ()
	{
	    _startPosition = transform.position;
	    _outbound = true;
	    _attractTarget = Vector2.zero;
	}
	
	// Update is called once per frame
	public void Update ()
	{
	    var attractor = FindObjectsOfType<Shell>().Where(s => s.Type == Shell.ShellType.SharkAttractant && (s.transform.position - transform.position).magnitude < 10).OrderBy(s=>(s.transform.position - transform.position).magnitude).Select(s=>s.gameObject).FirstOrDefault();

	    if (attractor == null)
	    {
	        var player = GameObject.Find("Player");
	        if (player.GetComponent<PlayerController>().ShellType == Shell.ShellType.SharkAttractant &&
	            (player.transform.position - transform.position).magnitude < 10)
	        {
	            attractor = player;
	        }
	    }

	    if (attractor != null && _attractTarget == Vector2.zero)
	    {
	        _attractTarget = attractor.transform.position + (Vector3)Random.insideUnitCircle * 2;
	    }

        Vector2 destination = _outbound ? PatrolPoint : _startPosition;

	    if (attractor != null)
	    {
	        destination = _attractTarget;
	    }

	    Vector2 movement = destination - (Vector2)transform.position;
	    if (movement.magnitude < Speed * Time.deltaTime)
	    {
	        transform.position = destination;
	        _outbound = !_outbound;
	        if (attractor != null)
	        {
	            _attractTarget = attractor.transform.position + (Vector3)Random.insideUnitCircle * 2;
	        }
        }
	    else
	    {
	        transform.position += (Vector3)movement.normalized * Speed * Time.deltaTime;
	    }


	    var scale = transform.localScale;

	    if (Mathf.Sign(scale.x) * Mathf.Sign(movement.x) > 0)
	    {
	        scale.x *= -1;
	        transform.localScale = scale;
	    }
        
	    var angle = Mathf.Rad2Deg * Mathf.Atan2(movement.y, movement.x) + 5f * Mathf.Sin(Time.time * 3 * Speed);

	    if (movement.x < 0)
	    {
	        angle += 180;
	    }

        transform.rotation = Quaternion.Euler(0,0, angle);
	}
}
