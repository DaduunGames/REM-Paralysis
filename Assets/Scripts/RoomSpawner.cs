using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    RoomController roomController; //reference to core brians of the generation proccess

    public bool CanSpawn = true;

    public RoomController.Direction direction; //each spawner needs to clarify it's individual direction

    public RoomSpawner SpawnedBy; //referencing the spawner that spawned this one in
    GameObject newRoom;



    void Start()
    {
        roomController = FindObjectOfType<RoomController>(); //only one roomcontroller in the scene
        
        SpawnedBy = GetComponentInParent<Room>().SpawnedByMain; //setting the room's spawned by to be found easier in it's anchors

        if (SpawnedBy != null) { SetCanSpawn(); } else { CanSpawn = true; }

        //start the spawning proccess
        if (CanSpawn)
        {
            Invoke("Spawn", roomController.spawnDelay);
        }

    }


    private void SetCanSpawn()
    {
        CanSpawn = true; //first set canspawn to true

        switch (direction) //then set canspawn to false if it meet ANY of these cases
        {
            //basically im just checking if *this* spawner is the opposite direction of the one that just spawned this room in
            //if so, that means i dont want to spawn ANOTHER room back int he directin i just came from
            case RoomController.Direction.Top: 
                if(SpawnedBy.direction == RoomController.Direction.Bottom)
                {
                    CanSpawn = false;
                }
                return;

            case RoomController.Direction.Bottom:
                if (SpawnedBy.direction == RoomController.Direction.Top)
                {
                    CanSpawn = false;
                }
                return;

            case RoomController.Direction.Left:
                if (SpawnedBy.direction == RoomController.Direction.Right)
                {
                    CanSpawn = false;
                }
                return;

            case RoomController.Direction.Right:
                if (SpawnedBy.direction == RoomController.Direction.Left)
                {
                    CanSpawn = false;
                }
                return;
        }
    }

    private void Spawn() 
    {
        //first we need to find the direction we're spawning in
        switch (direction)
        {
            case RoomController.Direction.Top: //E.G: this is a top spawner 
                SpawnRoom(roomController.BottomRooms, 1); //E.G: so we reference the rooms that have a bottom bridge
                return;

            case RoomController.Direction.Bottom:
                SpawnRoom(roomController.TopRooms, 0);
                return;

            case RoomController.Direction.Left:
                SpawnRoom(roomController.RightRooms, 3);
                return;

            case RoomController.Direction.Right:
                SpawnRoom(roomController.LeftRooms, 2);
                return;
        }
    }

    private void SpawnRoom(List<GameObject> rooms, int spawnDirection)
    {
        //if we're under the room minimum
        if (roomController.UnderMinimum) 
        {

            //pick a random room
            int rand = Random.Range(0, rooms.Count - 1);
            GameObject room = rooms[rand];

            //get new room anchor (so it spawns in the right position regardless of 0,0)
            GameObject anchor = room.GetComponent<Room>().Bridges_TBLR[spawnDirection].GetComponentInChildren<RoomSpawner>().gameObject;

            //spawn room
            newRoom = Instantiate(room, transform.position - anchor.transform.position, Quaternion.identity);

            //give the new room a reference to this spawner for later use
            newRoom.GetComponent<Room>().SpawnedByMain = this;
            anchor.GetComponent<RoomSpawner>().SpawnedBy = this;



        }
        //if we're over the room minimum
        else
        {
            Destroy(transform.parent.gameObject); //just destroy the bridge and dont let it spawn anything

        }
    }
}
