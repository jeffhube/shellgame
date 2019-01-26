using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkBehavior : MonoBehaviour
{
    public Vector2 PatrolPoint;
    public float Speed;

    private Vector2 _startPosition;
    private bool _outbound;

	// Use this for initialization
	void Start ()
	{
	    _startPosition = transform.position;
	    _outbound = true;
	}
	
	// Update is called once per frame
	public void Update ()
	{
	    Vector2 destination = _outbound ? PatrolPoint : _startPosition;

	    Vector2 movement = destination - (Vector2)transform.position;
	    if (movement.magnitude < Speed * Time.deltaTime)
	    {
	        transform.position = destination;
	        _outbound = !_outbound;
	    }
	    else
	    {
	        transform.position += (Vector3)movement.normalized * Speed * Time.deltaTime;
	    }


	    transform.localScale = new Vector3(-1 * Mathf.Sign(movement.x),1,1);

	    var angle = Mathf.Rad2Deg * Mathf.Atan2(movement.y, movement.x) + 5f * Mathf.Sin(Time.realtimeSinceStartup * 3 * Speed);

	    if (movement.x < 0)
	    {
	        angle += 180;
	    }

        transform.rotation = Quaternion.Euler(0,0, angle);
	}
}
