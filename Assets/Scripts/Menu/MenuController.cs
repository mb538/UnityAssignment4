using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    private bool gamePaused = false;

    [Header("Menu")]
    public GameObject pauseMenu;
    public GameObject gameUI;
    public GameObject gameOverMenu;

    private void Awake()
    {
        AudioListener.pause = false;
        pauseMenu.SetActive(true);
        gameUI.SetActive(true);
        gameOverMenu.SetActive(true);
    }

    void Start()
    {
        ResumeGame();
        HideCanvas();
        HideGameLost();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            ResumeGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            EventSystem.current.SetSelectedGameObject(null);
            FreezeGame();
            ShowCanvas();
        }

    }

    public void ResumeGame()
    {
        gamePaused = false;
        AudioListener.pause = false;
        Time.timeScale = 1;
        HideCanvas();
    }

    public void FreezeGame()
    {
        gamePaused = true;
        AudioListener.pause = true;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCanvas()
    {
        pauseMenu.GetComponent<Canvas>().enabled = true;
        pauseMenu.GetComponent<CanvasScaler>().enabled = true;
    }

    public void HideCanvas()
    {
        pauseMenu.GetComponent<Canvas>().enabled = false;
        pauseMenu.GetComponent<CanvasScaler>().enabled = false;
    }

    public void ShowGameLost()
    {
        gameOverMenu.GetComponent<Canvas>().enabled = true;
        gameOverMenu.GetComponent<CanvasScaler>().enabled = true;
    }
    public void HideGameLost()
    {
        gameOverMenu.GetComponent<Canvas>().enabled = false;
        gameOverMenu.GetComponent<CanvasScaler>().enabled = false;
    }
}