using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int SpawnDirection;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door
    public GameObject Bridge;
    public GameObject BridgeWall;

    private RoomTemplates templates;
    private int rand;

    private bool Spawned = false;

    public Transform Anchor;

    private GameObject spawnedroom;

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", templates.CheckDelay-0.2f);
    }

    void Spawn()
    {
        if (Spawned == false)
        {
            switch (SpawnDirection)
            {
                default:
                case 1: // need bottom door
                    SpawnRoom(templates.BottomRooms,0);

                    return;
                case 2: // need top door
                    SpawnRoom(templates.TopRooms,1);

                    return;
                case 3: // need left door
                    SpawnRoom(templates.LeftRooms,2);

                    return;
                case 4: // need right door
                    SpawnRoom(templates.RightRooms,3);

                    return;
            }
        }
    }

    private void SpawnRoom(GameObject[] roomList, int i)
    {
        if (templates.CanSpawn)
        {
            rand = Random.Range(0, roomList.Length);
            spawnedroom = Instantiate(roomList[rand], transform.position + Anchor.localPosition - roomList[rand].GetComponent<AddRoom>().SpawnPosition.localPosition, Quaternion.identity);
            
        }
        else
        {
            spawnedroom = Instantiate(templates.EndRooms[i], transform.position + Anchor.localPosition - templates.EndRooms[i].GetComponent<AddRoom>().SpawnPosition.localPosition, Quaternion.identity);
            
        }
        Spawned = true;
        spawnedroom.GetComponent<AddRoom>().RS = this;
        spawnedroom.GetComponent<AddRoom>().CanCheckforNull = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SpawnPoint" || collision.tag == "SpawnDeny")
        {
            if(!Spawned)
            {
                RemoveBridge();
            }
        }

    }

    public void RemoveBridge()
    {
        BridgeWall.SetActive(true);

        Destroy(Bridge);
        Destroy(gameObject);
    }
}
