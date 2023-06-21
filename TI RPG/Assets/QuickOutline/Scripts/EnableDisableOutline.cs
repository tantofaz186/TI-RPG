using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableOutline : MonoBehaviour
{
    private Outline outlineScript;

    private void Awake()
    {
        outlineScript = GetComponent<Outline>();
    }

    private void OnMouseEnter()
    {
        outlineScript.enabled = true;
    }

    private void OnMouseExit()
    {
        outlineScript.enabled = false;
    }
}
