using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;


    // Calling LateUpdate <- This runs after the update function so that it is not 
    //competing with the other update functions
    void LateUpdate()
    {
        // Current position of the player and the offset.
        Vector3 desiredPosition = target.position + offset;
        // Smooth the camera movement as it lerps towards the desiredPosition.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
