using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

	void Update ()
    {
        
		//Calls togglepause if key is pressed
        if (Input.GetKeyDown("tab"))
        {
            TogglePause();
        }
	}
	
	// Update is called once per frame
	public void TogglePause ()
    {

        //Changes game speed
        if (Time.timeScale ==1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
	}
}
