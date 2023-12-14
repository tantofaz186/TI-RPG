using UnityEngine;

public class ChecaInventarioAberto : MonoBehaviour
{
    [SerializeField]
    private GameObject inventarioGrande;

    [SerializeField]
    private GameObject botaoAbrirInventario;

    private void Update()
    {
        botaoAbrirInventario.SetActive(!inventarioGrande.activeSelf);
    }
}