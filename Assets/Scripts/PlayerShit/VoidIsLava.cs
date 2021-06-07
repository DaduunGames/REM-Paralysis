using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidIsLava : MonoBehaviour
{
    bool isTouchingGround;

    public PlayerMovement playerMovement;

    MyGameController gameController;

    RoomController roomController;

    public bool ShowOffset;
    public Vector3 offest;

    public RaycastHit2D hit;
    public GameObject currentHit;
    public GameObject PreviousHit;

    public float timeAllowance = 0.5f;


    private void Start()
    {
        gameController = FindObjectOfType<MyGameController>();
        roomController = FindObjectOfType<RoomController>();
    }

    // Update is called once per frame
    void Update()
    {


        hit = Physics2D.Raycast(transform.position + offest, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Hole")
            {
                isTouchingGround = false;
                //print(hit.collider.name);
            }
            else
            {
                isTouchingGround = true;
            }

        }
        else
        {
            isTouchingGround = false;
        }


        if (hit) currentHit = hit.collider.gameObject;  else currentHit = null; 
        if (currentHit != PreviousHit)
        {
            print("hit changed");
            Invoke("TestAgain", timeAllowance);
        }
        if (hit) PreviousHit = hit.collider.gameObject; else PreviousHit = null;
        
    }

    void TestAgain()
    {
        if (hit) currentHit = hit.collider.gameObject; else currentHit = null;

        if (currentHit == PreviousHit)
        {
            print("2nd hit also changed");
            if (isTouchingGround == false && roomController.finishedGenerating && !playerMovement.isDashing)
            {
                gameController.EndGame();

            }
        }
    }

    private void OnDrawGizmos()
    {
        if (ShowOffset)
        {
            Gizmos.DrawIcon(transform.position + offest, "");
        }
    }
}
