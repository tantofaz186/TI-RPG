using Rpg.Crafting;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public bool estaBloqueada;
    public Item chave;
    private AudioSource portaAudio;

    private void Awake()
    {
        portaAudio = GetComponent<AudioSource>();
        estaBloqueada = chave != null;
        if (!estaBloqueada)
        {
            DesativaNav();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || !PlayerInventory.Instance.HasItem(chave)) return;
        DesativaNav();
        portaAudio.Play();
    }

    private void DesativaNav()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        Destroy(chave);
        chave = null;
    }
}