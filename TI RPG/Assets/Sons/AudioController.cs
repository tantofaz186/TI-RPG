
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Controllers;
using UnityEngine.UI;

public class AudioController : MonoBehaviourSingletonPersistent<AudioController>
{
    public AudioMixer mixer;
    private float volMusica;
    public Slider vfxVol;
    public Slider musicaVol;
    public Slider masterVol;

    public float VolMusica
    {
        get { return volMusica; }
        set
        {

            volMusica = value;
            mixer.SetFloat("MusicaVol", volMusica);  
        }
    }

    public void MasterVol()
    {
        mixer.SetFloat("MasterVol", masterVol.value);
    }

    public void VFXVol()
    {
        mixer.SetFloat("VFXVol", vfxVol.value);
    }

    public void MusicaVol()
    {
        mixer.SetFloat("MusicaVol", musicaVol.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
