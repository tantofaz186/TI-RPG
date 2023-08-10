using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public bool estaBloqueada;
    public GameObject chave;
    AudioSource portaAudio;

    void DesativaNav()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        Destroy(chave);
        chave = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (chave != null)
        {
            if (other.CompareTag("Player") && chave.GetComponent<Coletavel>().Carregada == true)
            {
                DesativaNav();
                portaAudio.Play();
               
            }
        }
    }

    private void Awake()
    {
        portaAudio = GetComponent<AudioSource>();
        if (estaBloqueada == false)
        {
            DesativaNav();
        }
    }
}
