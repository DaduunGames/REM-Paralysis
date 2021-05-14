using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MyGameController : MonoBehaviour
{
    public Texture2D cursor;

    bool gameHasEnded = false;

    public float restartDelay = 1f;

    private void Start()
    {
        Cursor.SetCursor(cursor,new Vector2(cursor.width/2,cursor.height/2),CursorMode.Auto);
    }

    void EndGame()
    {
        if(gameHasEnded == true)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            //Game Over Screen
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
