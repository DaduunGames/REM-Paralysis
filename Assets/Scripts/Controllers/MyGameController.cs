using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameController : MonoBehaviour
{
    public Texture2D cursor;

    bool gameHasEnded = false;

    public float restartDelay = 1f;

    private PlayerMovement Player;

    public GameObject deathScreen;

    private void Start()
    {
        Player = FindObjectOfType<PlayerMovement>();
        Cursor.SetCursor(cursor,new Vector2(cursor.width/2,cursor.height/2),CursorMode.Auto);

        deathScreen.SetActive(false);
    }

    public void EndGame()
    {
        gameHasEnded = true;
        Debug.Log("GAME OVER");
        Player.stunTimer = 999;

        //Game Over Screen
        deathScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
