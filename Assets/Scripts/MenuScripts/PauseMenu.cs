using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public Animator animator;

    public AudioSource pauseAudio;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }

            else
            {
                Pause();
                pauseAudio.Play();
            }
        }
    }

   public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading Menu...");

    }

    public void QuitGame()
    {
        GameIsPaused = false;
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void PMOptions(bool PMToOptions)
    {

        animator.SetBool("PMToOptions", PMToOptions);
        Debug.Log("You fuck");
    }

    public void Help(bool ToHelp)
    {

        animator.SetBool("ToHelp", ToHelp);
    }

    public void DebugMenu(string text)
    {
        print(text);
    }
}

