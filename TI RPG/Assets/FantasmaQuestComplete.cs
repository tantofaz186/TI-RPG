using System.Collections.Generic;
using Refactor.Scripts.Quest;
using UnityEngine;

public class FantasmaQuestComplete : MonoBehaviour
{
    public Quest quest;
    public Dialogue dialogueOnStart;
    public Dialogue dialogueDuringQuest;
    public Dialogue dialogueOnComplete;
    public Dialogue dialogueAfterQuest;
    private DialogueTrigger dialogueTrigger;

    private void Awake()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.dialogue = dialogueOnStart;
    }

    private void OnEnable()
    {
        quest.OnComplete += OnQuestComplete;
        quest.objectives[0].OnComplete += OnObjectiveComplete;
        quest.objectives[1].OnComplete += OnSecondObjectiveComplete;
    }

    private void OnDisable()
    {
        quest.OnComplete -= OnQuestComplete;
        quest.objectives[0].OnComplete -= OnObjectiveComplete;
        quest.objectives[1].OnComplete -= OnSecondObjectiveComplete;
    }

    private void OnObjectiveComplete()
    {
        dialogueTrigger.dialogue = dialogueOnComplete;
    }

    private void OnSecondObjectiveComplete()
    {
        dialogueTrigger.dialogue = dialogueOnComplete;
    }

    private void OnQuestComplete(List<Rewards> obj)
    {
        dialogueTrigger.dialogue = dialogueAfterQuest;
    }
}