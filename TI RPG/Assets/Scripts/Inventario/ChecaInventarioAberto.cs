using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChecaInventarioAberto : MonoBehaviour
{
    [SerializeField]private GameObject inventarioGrande;
    [SerializeField]private GameObject botaoAbrirInventario;

    void Update()
    {
       if (SceneManager.GetActiveScene().name == "Calabouco")
        {
           if(inventarioGrande.activeSelf){
            botaoAbrirInventario.SetActive(false);
           }else{
            botaoAbrirInventario.SetActive(true);
           }
        }
    }
}
