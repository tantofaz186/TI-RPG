using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MedidoresPlayer : MonoBehaviour
{
    [SerializeField]
    private Slider medidorFolego;

    [SerializeField]
    private Slider medidorVidas;

    private PlayerAnimationController folegoUI;
    private PlayerDano vidasUI;

    private void Update()
    {
        MostrarFolegoEVida();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        folegoUI = FindObjectOfType<PlayerAnimationController>();
        vidasUI = FindObjectOfType<PlayerDano>();
    }

    private void MostrarFolegoEVida()
    {
        if (folegoUI == null || vidasUI == null) return;
        medidorFolego.value = folegoUI.folego;
        medidorVidas.value = vidasUI.Vidas;
    }
}