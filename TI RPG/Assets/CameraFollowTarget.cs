using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 basePosition;
    public float sensitivity = 5.0f; 
    private float currentAngle = 64.493f;
    private void Awake()
    {
        basePosition = transform.position - player.transform.position;
    }

    void Update () {
        transform.position = player.transform.position + basePosition;
        if (Input.GetMouseButtonDown(2))
        {
            currentAngle += Input.GetAxis("Mouse X") * sensitivity;
            Quaternion rotation = Quaternion.Euler(64.493f, currentAngle, 0);
            transform.rotation = rotation;
        }
    }

    
}
