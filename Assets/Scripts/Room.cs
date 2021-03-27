using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //stores the 4 set bridges (not allowed to delete bridges or add more bridges)
    public GameObject[] Bridges_TBLR;

    //RoomController is the core of the generation operation
    RoomController roomController;

    public RoomSpawner SpawnedByMain;

    bool SpawnSuccessful = false;

    void Start()
    {
        roomController = FindObjectOfType<RoomController>(); //there's only one room controller in the scene

        //as soon as this room gets spawned, add tot eh overall roomcount
        roomController.roomCount++;
        roomController.timer = roomController.spawnDelay;
        roomController.SpawnedRooms.Add(gameObject);
        SpawnSuccessful = false;
        Invoke("FinishSpawn", roomController.spawnDelay / 2);
    }

    private void FinishSpawn()
    {
        SpawnSuccessful = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Room" && !SpawnSuccessful) //only destroy self if this room isnt finished spawning yet
        {
            Invoke("destroyRoomAndBridge", 0.1f);
        }
    }

    private void destroyRoomAndBridge()
    {
        --roomController.roomCount;
        Destroy(SpawnedByMain.transform.parent.gameObject); //destroy the bridge that spawned you

        Destroy(gameObject); //destroy yourself
    }
}
