using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest Objective", menuName = "RPG/Quests/Objectives/Talk To NPC")]
    public class TalkToNpcObjective : QuestObjective
    {
        public DialogueTrigger npcToTalk;
        private DialogueTrigger _npcToTalk;

        public override void _OnEnable()
        {
            _npcToTalk = findInScene();
            _npcToTalk.endedDialogue += OnTalkToNpc;
            if (dialogueOnStart != null && dialogueOnStart.dialogues.Length > 0)
            {
                DialogueManager.Instance.StartDialogue(dialogueOnStart);
            }
        }

        private DialogueTrigger findInScene()
        {
            List<DialogueTrigger> dialogues = FindObjectsOfType<DialogueTrigger>().ToList();

            foreach (DialogueTrigger dialogue in dialogues)
            {
                if (dialogue.name == npcToTalk.name)
                {
                    return dialogue;
                }
            }

            return npcToTalk;
        }

        public override void _OnDisable()
        {
            _npcToTalk.endedDialogue -= OnTalkToNpc;
        }

        private void OnTalkToNpc()
        {
            CompleteObjective();
        }
    }
}