using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public GameObject peca;
    public bool ativado = false;

    public bool Ativado
    {
        get=> ativado;
    }
    MeshRenderer mr;
    private void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
    }
private void OnTriggerEnter(Collider other)
{
    if (other.gameObject == peca)
    {
        mr.material.EnableKeyword("_EMISSION");
        ativado = true;
        Puzzle.Instance.CompletarPuzzle();
        Debug.Log("Ativou");
    }
}

private void OnTriggerExit(Collider other)
{
    if (other.gameObject == peca)
    {
        mr.material.DisableKeyword("_EMISSION");
        ativado = false;
        Debug.Log("Desativou");
    }
}
 
}
