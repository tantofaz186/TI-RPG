using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPassos : MonoBehaviour
{
    private float footstepsHearingDistance = 5f;
    private float multiplier = 1f;
    public float FootstepsHearingDistance
    {
        get => footstepsHearingDistance * multiplier;
    }
    public float Multiplier
    {
        get => multiplier;
        set => multiplier = Mathf.Max(1,value);
    }
}
