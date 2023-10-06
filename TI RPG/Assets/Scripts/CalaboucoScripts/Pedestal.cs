using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public GameObject peca;
    public bool ativado = false;

    public bool Ativado
    {
        get => ativado;
    }

    MeshRenderer mr;

    private void Start()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == peca.gameObject)
        {
            mr.material.EnableKeyword("_EMISSION");
            ativado = true;
            Puzzle.Instance.CompletarPuzzle();
            Debug.Log("Ativou");
        }else{
            mr.material.DisableKeyword("_EMISSION");
            ativado = false;
            Debug.Log("Desativou");
        }
    }
}