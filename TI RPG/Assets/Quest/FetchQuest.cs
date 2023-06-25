using Objetos;
using Player;
using Skills;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FetchQuest : Interagível
{
    public Dialogue dialogueInicio;
    public Dialogue dialogueLembrarMissao;
    public Dialogue dialogueFim;
    public bool estaComItemQuest;
    public bool questConcluida; 

    private int interactionCount = 0;

    public void TriggerDialogueQuest()
    {
        player.Mover(player.transform.position);

        if (interactionCount == 0 && !estaComItemQuest)
        {
            DialogueManager.Instance.StartDialogue(dialogueInicio);
            interactionCount++;
        }
        else if (interactionCount == 1 && !estaComItemQuest)
        {
            DialogueManager.Instance.StartDialogue(dialogueLembrarMissao);
            interactionCount++;
        }
        else if (interactionCount >=0  && estaComItemQuest)
        {
            DialogueManager.Instance.StartDialogue(dialogueFim);
            interactionCount=1;
            questConcluida = true;
            XpPlayer xpPlayer = FindObjectOfType<XpPlayer>();
            xpPlayer._xpAtual += 1;
            QuestManager manager = QuestManager.Instance;
            manager.AtualizarQuests();
        }


        if (interactionCount >= 2)
            interactionCount = 1;

        if (SceneManager.GetActiveScene().name == "Tutorial")
            return;
        Debug.Log(interactionCount);
    }

    protected override void Interagir() => TriggerDialogueQuest();
}