using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    public Transform target; // The object to orbit around
    public float distance = 0.0f; // The distance from the object
    public float sensitivity = 5.0f; // The speed of rotation

    private float currentAngle = 64.493f;

    void Update()
    {
        currentAngle += Input.GetAxis("Mouse X") * sensitivity;
        Quaternion rotation = Quaternion.Euler(64.493f, currentAngle, 0);
        Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;
        transform.rotation = rotation;
        transform.position = position;
    }

}
