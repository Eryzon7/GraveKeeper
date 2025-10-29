using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform cameraTransform;

    void LateUpdate()
    {
        Vector3 newPos = cameraTransform.position;
        newPos.y = newPos.y + 5; // keep emitter height constant
        transform.position = newPos;
    }
}