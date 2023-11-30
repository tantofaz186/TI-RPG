using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public GameObject peca;
    public bool ativado;

    private MeshRenderer mr;

    public bool Ativado => ativado;

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
        }
        else
        {
            mr.material.DisableKeyword("_EMISSION");
            ativado = false;
        }
    }
}