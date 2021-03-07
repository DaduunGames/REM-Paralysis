using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    RoomTemplates templates;

    public Transform SpawnPosition;

    public GameObject RoomDenier;

    public RoomSpawner RS;
    public bool CanCheckforNull = false;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.Rooms.Add(this.gameObject);
    }

    private void Update()
    {
        if(RS == null && CanCheckforNull)
        {
            DestroySelf();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SpawnDeny" && collision.gameObject.layer != 8)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        if(RS != null) RS.RemoveBridge();
        templates.RemoveRoom(this.gameObject);
    }

}
