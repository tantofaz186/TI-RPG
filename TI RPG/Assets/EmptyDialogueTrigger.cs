using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class EmptyDialogueTrigger : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] private GameObject changeScene;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.gameObject.GetComponent<PlayerMovement>().Mover(other.transform.position - Vector3.back );
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnDisable()
    {
        changeScene.SetActive(true);
    }
}
