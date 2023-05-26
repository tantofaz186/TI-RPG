using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 lastPlayerPosition;
    public float sensitivity = 5.0f;
    private float currentAngle;

    private void Awake()
    {
        lastPlayerPosition = player.position;
    }

    void Update () {
        if (Input.GetMouseButton(2))
        {
            currentAngle = Input.GetAxis("Mouse X") * sensitivity;
            transform.RotateAround (player.position, Vector3.up , currentAngle);
            
        }
        transform.Translate(player.position - lastPlayerPosition, Space.World);
        lastPlayerPosition = player.position;
    }

    
}
