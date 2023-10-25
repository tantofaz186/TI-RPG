using System;
using Objetos;


public class DialogueTrigger : Interagível
{
    //TODO fazer madeira ser usada para uma fogueira também
    public Dialogue dialogue;
    private bool used = false;
    public Action triggeredDialogue;
    public Action endedDialogue;
    public void TriggerDialogue()
    {
        player.Mover(player.transform.position);
        DialogueManager.Instance.StartDialogue(dialogue);
        triggeredDialogue?.Invoke();
        DialogueManager.Instance.endDialogue = EndDialogue;
    }

    private void EndDialogue()
    {
        endedDialogue?.Invoke();
    }

    protected override void Interagir() => TriggerDialogue();
}
