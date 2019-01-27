using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {
	
	// Advance to next screen on anykey
	void Update ()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("LevelSelect");
        }
		
	}
}
