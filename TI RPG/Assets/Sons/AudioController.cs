
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioController : MonoBehaviour
{
    public AudioMixer controlador;
    static public AudioController audio;
    private float volMusica;

    public float VolMusica
    {
        get { return volMusica; }
        set
        {

            volMusica = value;
            controlador.SetFloat("VolumeMixer", volMusica);  
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audio = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
