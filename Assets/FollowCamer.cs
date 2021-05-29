using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamer : MonoBehaviour
{
    public GameObject player;
    public float smoothness;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 targetPosition = player.transform.TransformPoint(new Vector3(0, 0, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
    }
}
