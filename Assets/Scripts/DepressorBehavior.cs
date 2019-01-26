using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressorBehavior : MonoBehaviour
{
    public bool Triggered;

    private SliderJoint2D _slider;
	// Use this for initialization
	void Start ()
	{
	    _slider = GetComponent<SliderJoint2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Triggered = _slider.jointTranslation >= 0.95f * _slider.limits.max;
	}
}
