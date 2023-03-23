using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject settingsPanel;
    
    void Start()
    {
        // if we decide to use the main menu as a settings scene while the game is active, delete these lines.
        // otherwise, the title screen will be active each time the scene is loaded.
        titleScreen.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void StartGame()
    {
        // Start first level or load save
        SceneManager.LoadScene("Level01");
    }

    public void Settings()
    {
        titleScreen.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
