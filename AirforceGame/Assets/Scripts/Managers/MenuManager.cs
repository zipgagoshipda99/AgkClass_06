using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void QuitGame()
    {
        //basically adding # means do not run this code to unity when the game is playing.
        //preprocessor code runs before play.
        #if UNITY_EDITOR
        // This stops play mode in the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
            // This closes the actual built application
        #else
            Application.Quit();
        #endif
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    // Update is called once per frame
    void Start()
    {
        Time.timeScale = 1f;
    }
}
