using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    [SerializeField] String nomeCena;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneChanger.Instance.MudaCena(nomeCena);
        }
    }
}
