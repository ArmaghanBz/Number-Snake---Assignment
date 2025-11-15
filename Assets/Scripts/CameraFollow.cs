using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;         // Player head
    public Vector3 offset = new Vector3(0, 5, -8);
    public float followSpeed = 5f;
    public float rotationLerp = 5f;

    void LateUpdate()
    {
        if (!target) return;

        // Smooth position
        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, followSpeed * Time.deltaTime);

        // Smooth rotation (look at player)
        Quaternion desiredRot = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, rotationLerp * Time.deltaTime);
    }
}
