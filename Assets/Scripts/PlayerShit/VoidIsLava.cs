using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidIsLava : MonoBehaviour
{
    bool isTouchingGround;

    public PlayerMovement playerMovement;

    MyGameController gameController;

    RoomController roomController;

    private void Start()
    {
        gameController = FindObjectOfType<MyGameController>();
        roomController = FindObjectOfType<RoomController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouchingGround ==  false && roomController.finishedGenerating && !playerMovement.isDashing)
        {
            gameController.EndGame();
            //Debug.Log("Working?");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Floor")
        {
            isTouchingGround = true;
  
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Floor")
        {
            isTouchingGround = false;
           
        }
    }
}
