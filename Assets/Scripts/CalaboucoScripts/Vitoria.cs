using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;

public class Vitoria : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
            Debug.Log("Venceu");
            VitoriaController.Instance.Ganhou();
    }


}

