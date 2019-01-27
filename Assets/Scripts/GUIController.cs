using UnityEngine;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    //Fields
    public GameObject pauseOverlay;

    void Start()
    {
        if (pauseOverlay == null)
        {
            pauseOverlay = GameObject.Find("PauseCanvas");
        }

        if (pauseOverlay != null)
        {
            pauseOverlay.SetActive(false);
        }
    }

    void Update ()
    {
        //Various calls for different keys. Triggers can be easily
        //changed/moved



		//Calls togglepause if key is pressed
        if (Input.GetKeyDown("tab"))
        {
            TogglePause();
        }

        //Calls Restart Level
        if (Input.GetKeyDown("escape"))
        {
            RestartLevel();
        }
	}
	
	//Pause or unpause the game; changes timescale
	private void TogglePause ()
    {

        //Changes game speed, toggles overlay
        if (Time.timeScale ==1)
        {
            Time.timeScale = 0;
            pauseOverlay.SetActive(true);
            
        }
        else
        {
            Time.timeScale = 1;
            pauseOverlay.SetActive(false);
        }
	}

    //Restart the current level. Only functions while paused.
    private void RestartLevel()
    {
        if (Time.timeScale == 0)
        {
            Application.LoadLevel(Application.loadedLevel);
            TogglePause();
        }

    }
}
