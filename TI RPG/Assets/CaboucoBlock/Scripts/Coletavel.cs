using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField]private GameObject player;
    [SerializeField] private GameObject mao;
    [SerializeField] public string maoNome;
    bool carregada=false;
   
    public bool Carregada
    {
        get { return carregada; }
    }

    void Pegar()
    {
        carregada = true;
        this.transform.parent = mao.transform;
        gameObject.transform.position= transform.parent.position;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
        Debug.Log("pegou");
    }

    void Largar()
    {
        this.transform.parent = null;
        gameObject.GetComponent<Rigidbody>().isKinematic = false; ;
        carregada = false;
    }

    GameObject EncontrarMao(GameObject player, string nome)
    {
        for(int i = 0; i < (player.transform.childCount); i++)
        {
            if(player.transform.GetChild(i).name == nome)
            {
                return player.transform.GetChild(i).gameObject;
            }

            GameObject aux = EncontrarMao(player.transform.GetChild(i).gameObject, maoNome);

            if (aux != null)
            {
                return aux;
            }
        }
        return null;

    }
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mao=EncontrarMao(player, maoNome);
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.E) == true && (transform.position - player.transform.position).magnitude <= 2.0f && carregada == false )
         {
            Pegar();
         }

        else if (carregada == true && Input.GetKeyDown(KeyCode.E) == true)
        {
            Largar();
        }
    }
}
