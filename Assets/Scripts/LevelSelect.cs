using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour {

	// Loads the specified levelname
	public void LoadLevel (string levelName)
    {
        SceneManager.LoadScene(levelName);
	}

    public void SomeMethod()
    {

    }
}
