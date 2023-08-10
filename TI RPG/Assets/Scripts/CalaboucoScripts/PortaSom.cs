using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaSom : MonoBehaviour
{
    AudioSource portaAudio;

    private void Awake()
    {
        portaAudio = GetComponent<AudioSource>();
    }

    private IEnumerator TocaSom()
    {
         portaAudio.Play();
        yield return new WaitForSeconds(2.0f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Inimigo"))
        {
            StartCoroutine("TocaSom");
        } 
    }
}
