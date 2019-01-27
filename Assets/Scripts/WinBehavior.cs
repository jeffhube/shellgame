using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinBehavior : MonoBehaviour
{

    private float _transitionTimer;

	// Use this for initialization
	void Start ()
    {
		_transitionTimer = 4;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    _transitionTimer -= Time.deltaTime;
	    if (_transitionTimer < 0)
	    {
	        SceneManager.LoadScene("LevelSelect");
	        _transitionTimer = 10;
	    }
	}
}
