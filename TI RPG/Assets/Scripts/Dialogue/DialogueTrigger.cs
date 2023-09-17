using System.Collections;
using System.Collections.Generic;
using Objetos;
using Player;
using Skills;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : Interagível
{
    public Dialogue dialogue;
    private bool used = false;
    public void TriggerDialogue()
    {
        player.Mover(player.transform.position);
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    protected override void Interagir() => TriggerDialogue();
}
