using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public bool isFollowing = true;

    void Update()
    {
        if (!target)
        {
            return;
        }
        if (isFollowing)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
            Vector3 smoothedRotation = Vector3.Lerp(transform.rotation.eulerAngles, new Vector3(60, 0, 0), smoothSpeed);
            transform.rotation = Quaternion.Euler(smoothedRotation);
        }
        else
        {
            transform.LookAt(target);
        }
    }
}