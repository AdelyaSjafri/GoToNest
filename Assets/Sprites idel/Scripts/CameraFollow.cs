using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform PlayerTransform;
    public Vector3 offset;
    public float smootSpeed = 0.125f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = PlayerTransform.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smootSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

}
