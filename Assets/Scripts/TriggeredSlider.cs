using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggeredSlider : MonoBehaviour
{
    public enum Direction { Horizontal, Vertical}

    public DepressorBehavior Trigger;
    public Direction SlideDirection;
    public float SlideDistance;

    private Vector3 _initialPosition;
    private Vector3 _targetPosition;

    // Use this for initialization
    void Start ()
	{
	    _initialPosition = transform.position;
	    _targetPosition = _initialPosition;
	    if (SlideDirection == Direction.Horizontal)
	    {
	        _targetPosition += Vector3.right * SlideDistance;
	    }
	    else
	    {
	        _targetPosition += Vector3.up * SlideDistance;
	    }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 destination = Trigger.Triggered ? _targetPosition : _initialPosition;

	    Vector2 movement = destination - transform.position;
	    if (movement.magnitude < 5 * Time.deltaTime)
	    {
	        transform.position = destination;
	    }
	    else
	    {
	        transform.position += (Vector3)movement.normalized * 5 * Time.deltaTime;
	    }

    }
}
