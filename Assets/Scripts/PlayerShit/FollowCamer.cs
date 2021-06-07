using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamer : MonoBehaviour
{
    public GameObject player;
    public float smoothness;
    private Vector3 velocity = Vector3.zero;
    public float CamShakeIntensity = 5;

    void Update()
    {
        Vector3 targetPosition = player.transform.TransformPoint(new Vector3(0, 0, -10));
        if (player.GetComponent<PlayerMovement>().IsStunned)
        {
            Random.seed = (int)Time.frameCount;
            targetPosition += (Random.insideUnitSphere * CamShakeIntensity);

            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness/10);
        }
        else
        {
            // Smoothly move the camera towards that target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
        }

        
        
    }
}
