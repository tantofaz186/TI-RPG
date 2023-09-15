using Objetos;
using Player;
using Skills;
using UnityEngine;
using UnityEngine.SceneManagement;

<<<<<<< HEAD
public class FetchQuest : Interagível
=======
public class FetchQuest : InteragÃ­vel
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
{
    public Dialogue dialogueInicio;
    public Dialogue dialogueLembrarMissao;
    public Dialogue dialogueFim;
    public bool estaComItemQuest;
    public bool questConcluida; 
    public QuestItem questItem;
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
            interactionCount = 1;
            questConcluida = true;
            SkillManager.Instance.GetComponent<XpPlayer>().AddXp();
            QuestManager.Instance.AtualizarQuests();
            questItem.ColocarNaMaoDoGameObject(gameObject,"mixamorig:RightHand");
        }


        if (interactionCount >= 2)
            interactionCount = 1;

        if (SceneManager.GetActiveScene().name == "Tutorial")
            return;
        Debug.Log(interactionCount);
    }

    protected override void Interagir() => TriggerDialogueQuest();
}