using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public GameObject peca;
    bool ativado = false;

    public bool Ativado
    {
        get=> ativado;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject==peca)
        {
            gameObject.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            ativado = true;
            Puzzle.puzzle.CompletarPuzzle();
            Debug.Log("Ativou");
        }
        
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == peca)
        {
            gameObject.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            ativado = false;
            Debug.Log("Desativou");
        }
    }
 
}
