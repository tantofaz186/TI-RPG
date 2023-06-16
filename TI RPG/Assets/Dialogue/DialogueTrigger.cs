using System.Collections;
using System.Collections.Generic;
using Objetos;
using Player;
using UnityEngine;

public class DialogueTrigger : Interagível
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        player.Mover(player.transform.position);
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    protected override void Interagir() => TriggerDialogue();
}
