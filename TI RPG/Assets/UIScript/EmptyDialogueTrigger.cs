using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EmptyDialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] private GameObject changeScene;
    [SerializeField] private GameObject Pedestal;
    private void OnTriggerEnter(Collider other)
    {
        if (Pedestal.GetComponent<Pedestal>().Ativado)
        {
            gameObject.SetActive(false);
            return;
        }
        if (!other.CompareTag("Player")) return;
        other.gameObject.GetComponent<PlayerMovement>().Mover(other.transform.position - Vector3.back );
        other.gameObject.GetComponent<PlayerMovement>().transform.position = other.transform.position - Vector3.back;
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnDisable()
    {
        changeScene?.SetActive(true);
    }
}
