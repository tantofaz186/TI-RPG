using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField]Vector3 basePosition;
    // Update is called once per frame
    private void Awake()
    {
        basePosition = transform.position - player.transform.position;
    }

    void Update () {
        transform.position = player.transform.position + basePosition;
    }

}
