using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Room : MonoBehaviour
{
    //stores the 4 set bridges (not allowed to delete bridges or add more bridges)
    public GameObject[] Bridges_TBLR;

    //RoomController is the core of the generation operation
    RoomController roomController;

    public RoomSpawner SpawnedByMain;

    bool SpawnSuccessful = false;

    //public AstarPath aStar;
    public int Astarwidth = 20;
    public int Astardepth = 20;
    public float AstarnodeSize = 0.25f;

    public bool Startingroom = false;
    

    void Start()
    {
        roomController = FindObjectOfType<RoomController>(); //there's only one room controller in the scene

        //as soon as this room gets spawned, add tot eh overall roomcount
        roomController.roomCount++;
        roomController.timer = roomController.spawnDelay;
        roomController.SpawnedRooms.Add(gameObject);
        SpawnSuccessful = false;
        Invoke("FinishSpawn", roomController.spawnDelay / 2);

        // This holds all graph data
        
        
        
        
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
        roomController.SpawnedRooms.Remove(gameObject);

        --roomController.roomCount;
        Destroy(SpawnedByMain.transform.parent.gameObject); //destroy the bridge that spawned you

        Destroy(gameObject); //destroy yourself
    }

    public void createAstarGraph()
    {
        AstarPath aStar = FindObjectOfType<AstarPath>();
        AstarData data = aStar.data;

        // This creates a Grid Graph
        GridGraph gg = data.AddGraph(typeof(GridGraph)) as GridGraph;

        // Setup a grid graph with some values
        gg.collision.use2D = true;
        gg.rotation = new Vector3(90, 0, 0);
        gg.center = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
        // Updates internal size from the above values
        gg.SetDimensions(Astarwidth, Astardepth, AstarnodeSize);

        //gg.collision.mask = LayerMask.GetMask("Ground");

        
        // Scans all graphs
        AstarPath.active.Scan();
    }
}
