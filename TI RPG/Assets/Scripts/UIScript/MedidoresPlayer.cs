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

    private PlayerMovement folegoUI;
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
        folegoUI = FindObjectOfType<PlayerMovement>();
        vidasUI = FindObjectOfType<PlayerDano>();
        DesativaNoMenu(scene, mode);
    }

    private void MostrarFolegoEVida()
    {
        if (folegoUI == null || vidasUI == null) return;
        medidorFolego.value = folegoUI.folego;
        medidorVidas.value = vidasUI.Vidas;
    }

    private void DesativaNoMenu(Scene arg0, LoadSceneMode loadSceneMode)
    {
        medidorFolego.gameObject.SetActive(arg0.buildIndex != 0);
        medidorVidas.gameObject.SetActive(arg0.buildIndex != 0);
    }
}