using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player;

public class MedidoresPlayer : MonoBehaviour
{

    [SerializeField] private PlayerMovement folegoUI;
    [SerializeField] private PlayerDano vidasUI;
    [SerializeField] private Slider medidorFolego;
    [SerializeField] private Slider medidorVidas;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if(folegoUI==null){
        //     folegoUI=GameObject.FindObjectOfType(typeof(PlayerMovement));
        // }
    }

    private void Update()
    {
        MostrarFolegoEVida();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += DesativaNoMenu;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= DesativaNoMenu;
    }

    private void MostrarFolegoEVida()
    {
        medidorFolego.value=folegoUI.folego;
        medidorVidas.value=vidasUI.Vidas;
    }

    private void DesativaNoMenu(Scene arg0, LoadSceneMode loadSceneMode)
    {
        //medidor.gameObject.SetActive(arg0.buildIndex != 0);
        medidorFolego.gameObject.SetActive(arg0.buildIndex != 0);
        medidorVidas.gameObject.SetActive(arg0.buildIndex != 0);
    }
}