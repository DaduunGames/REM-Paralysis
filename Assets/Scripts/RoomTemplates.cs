using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public int minRooms;
    
    public GameObject[] TopRooms;
    public GameObject[] BottomRooms;
    public GameObject[] RightRooms;
    public GameObject[] LeftRooms;

    public List<GameObject> Rooms;

    public GameObject StartRoom;
    public GameObject[] EndRooms;

    public GameObject Boss;
    //public GameObject TestMarker;

    private bool spawnedBoss = false;
    public int currentRooms = 0;
    public float CheckDelay = .2f;
    private float Timer;
    private bool IsResetting = false;
    public bool CanSpawn = true;

    private void Start()
    {
        Timer = CheckDelay;
        SpawnStartRoom();
    }

    private void Update()
    {
        if (!IsResetting)
        {
            if (Timer <= 0 && !spawnedBoss)
            {
                if (currentRooms < minRooms)
                {
                    ResetLevelGeneration();
                }
                else
                {
                    spawnedBoss = true;
                    Instantiate(Boss, Rooms[Rooms.Count - 1].transform.position, Quaternion.identity);
                }

            }
            else
            {
                Timer -= Time.deltaTime;
            }


            if (Rooms.Count > currentRooms)
            {
                Timer = CheckDelay;
                currentRooms++;
            }


        }

        if (currentRooms > minRooms && CanSpawn)
        {
            CanSpawn = false;
        }
        
    }

    public void ResetLevelGeneration()
    {
        IsResetting = true;


        currentRooms = 0;
        foreach(GameObject room in Rooms)
        {
            Destroy(room);
        }
        Rooms.Clear();

        SpawnStartRoom();
        Timer = CheckDelay;

        IsResetting = false;
    }

    private void SpawnStartRoom()
    {
        Instantiate(StartRoom, transform.position, Quaternion.identity);
    }

    public void RemoveRoom(GameObject room)
    {
        currentRooms--;
        Rooms.Remove(room);
        Destroy(room);
        Timer = CheckDelay;
    }
}
