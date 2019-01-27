using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

	// Loads the specified levelname
	public void LoadLevel (string levelName)
    {
        SceneManager.LoadScene(levelName);
	}

    public void SomeMethod()
    {

    }
}
