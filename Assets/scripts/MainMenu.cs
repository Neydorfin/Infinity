using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;
    public string secondLevel;

    public GameObject optionScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Level1()
    {
        // Cursor.visible = false;
        Cursor.lockState =  CursorLockMode.Locked;
        SceneManager.LoadScene(firstLevel);
    }

    public void Level2()
    {
        // Cursor.visible = false;
        Cursor.lockState =  CursorLockMode.Locked;
        SceneManager.LoadScene(secondLevel);
    }

    public void OpenOption()
    {
        optionScreen.SetActive(true);
    }

    public void CloseOption()
    {
        optionScreen.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
