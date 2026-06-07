using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public float worldSpeed = 1.25f;
    public static GameManager gameManager;

    void Awake()
    {
        if(gameManager != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gameManager = this;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetButtonDown("Fire3"))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        if(UI_Controller.uiController.pausePanel.activeSelf == false)
        {
            UI_Controller.uiController.pausePanel.SetActive(true);
            Time.timeScale = 0f;
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.pause);
        }
        else
        {
            UI_Controller.uiController.pausePanel.SetActive(false);
            Time.timeScale = 1f;
            PlayerManager.playerManager.ExitBoost();
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.unPause);
        }
    }
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
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainGameMenu");
    }
    public IEnumerator DelayShowGameOverScreen()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
        
    }
}
