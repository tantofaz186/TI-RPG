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

    private void OnEnable()
    {
        PlayerInventory.Instance.onAddItem += OnChaveAdded;
    }

    private void OnDisable()
    {
        PlayerInventory.Instance.onAddItem -= OnChaveAdded;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        portaAudio.Play();
    }

    private void OnChaveAdded(Item obj)
    {
        if (obj != chave) return;
        estaBloqueada = false;
        DesativaNav();
    }

    private void DesativaNav()
    {
        PlayerInventory.Instance.onAddItem -= OnChaveAdded;
        transform.GetChild(2).gameObject.SetActive(false);
    }
}