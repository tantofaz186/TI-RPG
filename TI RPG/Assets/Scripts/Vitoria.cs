using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public class Vitoria : MonoBehaviour
{
    public GameObject chave;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == chave)
        {
            Debug.Log("Venceu");
            GameOverController.Instance.GameOver();
        }
    }


}

