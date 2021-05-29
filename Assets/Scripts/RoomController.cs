using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RoomController : MonoBehaviour
{
    //room limit logic
    public int MinRooms = 5;
    public int MaxRooms = 10;
    public int roomCount = 0;
    [HideInInspector]
    public List<GameObject> SpawnedRooms;

    [Range(0,100)]
    public float bridgeSpawnChance = 50;

    //prefab storage
    public GameObject[] AllRooms;
    //sorting prefabs into meaningful lists (done automatically through AllRooms[]) 
    //(only public because other scripts need to reference it. you dont need to put anything into it in inspector)
    [HideInInspector]
    public List<GameObject> TopRooms;
    [HideInInspector]
    public List<GameObject> BottomRooms;
    [HideInInspector]
    public List<GameObject> LeftRooms;
    [HideInInspector]
    public List<GameObject> RightRooms;
    //misc prefabs that need manual sorting
    //public GameObject[] deadEnds;
    public bool GenerateLevelOnStart;
    public GameObject StartRoom;
    public GameObject BossPortal;

    public AstarPath Astar;

    //room spawn logic
    public bool UnderMinimum = true;
    public float spawnDelay = 2;

    //end generation checks
    public float timer;
    bool SpawnedBoss = false;
    bool IsResetting = false;

    public bool finishedGenerating;
    public bool coverWithLoadingScreen;
    public GameObject loadingScreen;
   
    public enum Direction
    {
        //universal Enum to reference for direction (useful for switch cases and lists that need ints)
        Top = 0,
        Bottom = 1,
        Left = 2,
        Right = 3
    };

    void Start()
    {
        timer = spawnDelay; //start timer

        sortRooms(); //sort the rooms into the 4 different lists above
        SpawnStartRoom(); //spawn the starting room which will begin the map generation

        finishedGenerating = false;
    }


    void Update()
    {
        if (!IsResetting)
        {
            // stop spawning more random rooms once it's passed the minimum room count
            // you can end up with 10 or so more rooms after this
            // this is because once "CanSpawn" is set to false, 
            // you still have to fill all the openings with dead end rooms
            if (roomCount > MinRooms)
            {
                UnderMinimum = false;
            }

            if (roomCount > 100 || roomCount < -100) //if the room spawning goes haywire, stop it before unity freazes
            {
                Debug.Break();
            }

            if (!SpawnedBoss)
            {
                if (timer <= 0)
                {
                    if (roomCount < MinRooms || roomCount > MaxRooms)
                    {
                        //reset logic here
                        ResetLevelGeneration();
                    }
                    else
                    {
                        FinishedGeneration();
                    }
                }
                else
                {
                    timer -= Time.deltaTime;
                }
            }

        }

        if (coverWithLoadingScreen)
        {
            if (finishedGenerating)
            {
                loadingScreen.SetActive(false);
            }
            else
            {
                loadingScreen.SetActive(true);
            }
        }
    }

    private void sortRooms()
    {
        foreach(GameObject room in AllRooms) //look at every prefab
        {
            Room roomScript = room.GetComponent<Room>(); //get a reference to the "Room" script
            if (roomScript == null)
            {
                //someone put a prefab in that WASNT a room, get that prefab out of the array!!!
                Debug.LogError($"There was no Room Script found on {room.name}");
                return;
            }


            //E.G: check if the room has an active "Top" connecting bridge. If so, add it to the "TopRooms" list
            //E.G: if a room has a top bridge and a left bridge, it will be added to BOTH of them.
            if (roomScript.Bridges_TBLR[(int)Direction.Top].activeSelf)
            {
                TopRooms.Add(room);
            }
            if (roomScript.Bridges_TBLR[(int)Direction.Bottom].activeSelf)
            {
                BottomRooms.Add(room);
            }
            if (roomScript.Bridges_TBLR[(int)Direction.Left].activeSelf)
            {
                LeftRooms.Add(room);
            }
            if (roomScript.Bridges_TBLR[(int)Direction.Right].activeSelf)
            {
                RightRooms.Add(room);
            }
        }
    }

    public void ResetLevelGeneration()
    {
        IsResetting = true;

        roomCount = 0;
        
        foreach(GameObject room in SpawnedRooms)
        {
            Destroy(room);
        }
        SpawnedRooms.Clear();

        UnderMinimum = true;

        SpawnStartRoom();
        timer = spawnDelay;

        IsResetting = false;

        SpawnedBoss = false;

        if (!Astar.data.graphs.Equals(null))
        {
            foreach (NavGraph gg in Astar.data.graphs)
            {
                Astar.data.RemoveGraph(gg);
            }
        }
    }

    private void SpawnStartRoom()
    {
        if (GenerateLevelOnStart) {
            //super simple "spawn" for the very virst starting room right at 0,0 with no rotation.
            Instantiate(StartRoom, transform.position, Quaternion.identity);
        }
    }

    private void FinishedGeneration()
    {
        print("finished generation");
        SpawnedBoss = true;

        foreach(GameObject room in SpawnedRooms)
        {
            if (room.GetComponent<Room>() != null)
            room.GetComponent<Room>().createAstarGraph();
        }

        GameObject lastRoom = SpawnedRooms[SpawnedRooms.Count - 1];
        RoomSpawner rs = lastRoom.GetComponent<Room>().SpawnedByMain;

        Destroy(lastRoom);
        rs.SpawnBossPortal();

        AstarPath.active.Scan();





        finishedGenerating = true;
    }
}
