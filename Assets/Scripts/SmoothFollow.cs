using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform TargetPos;
    public Vector3 Offset;

    [Range (0,1)]
    public float SmoothStrength = 0.125f;

    void LateUpdate()
    {
        Vector3 DirectPos = TargetPos.position + Offset;
        Vector3 SmoothPos = Vector3.Lerp(transform.position, DirectPos, SmoothStrength);

        transform.position = SmoothPos;
    }
}
