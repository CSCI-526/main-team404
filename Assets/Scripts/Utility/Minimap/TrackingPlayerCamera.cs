using UnityEngine;

public class TrackingPlayerCamera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        Vector3 nextPostioon = player.position + offset;
        Vector3 soomthPosition = Vector3.Lerp(transform.position, nextPostioon, smoothSpeed);
        transform.position = soomthPosition;
        transform.LookAt(transform.position);
    }
}
