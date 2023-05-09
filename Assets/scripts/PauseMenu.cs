using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public GameObject optionScreen;
    public string MainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                rezumeGame();
            }
            else
            {
                // Cursor.visible = true;
                Cursor.lockState =  CursorLockMode.None;
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void rezumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        // Cursor.visible = false;
        Cursor.lockState =  CursorLockMode.Locked;
    }

    public void returnToMain()
    {
        // Cursor.visible = true;
        Cursor.lockState =  CursorLockMode.None;
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuScene);
    }

    
    public void OpenOption()
    {
        // Cursor.visible = true;
        Cursor.lockState =  CursorLockMode.None;
        optionScreen.SetActive(true);
    }

    public void CloseOption()
    {
        optionScreen.SetActive(false);
    }

}
