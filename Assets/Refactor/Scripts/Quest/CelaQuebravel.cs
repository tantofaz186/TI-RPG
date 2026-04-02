using System.Collections.Generic;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    public class CelaQuebravel : MonoBehaviour
    {
        public Quest quest;
        public Dialogue dialogueOnStart;
        public Dialogue dialogueOnComplete;
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
            if (quest.IsCompleted)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            quest.OnComplete -= OnQuestComplete;
            quest.objectives[0].OnComplete -= OnObjectiveComplete;
        }

        private void OnObjectiveComplete()
        {
            dialogueTrigger.dialogue = dialogueOnComplete;
        }

        private void OnQuestComplete(List<Rewards> rewardsList)
        {
            gameObject.SetActive(false);
        }
    }
}