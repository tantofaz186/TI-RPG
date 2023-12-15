using System.Collections.Generic;
using System.Linq;
using Rpg.Crafting;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest Objective", menuName = "RPG/Quests/Objectives/Deliver Object")]
    public class DeliverObjectObjective : QuestObjective
    {
        public Item objectToDeliver;
        public DialogueTrigger npcToDeliver;
        private DialogueTrigger _npcToDeliver;

        public override void _OnEnable()
        {
            _npcToDeliver = findInScene();
            _npcToDeliver.endedDialogue += OnTalkToNpc;
        }

        private DialogueTrigger findInScene()
        {
            List<DialogueTrigger> dialogues = FindObjectsOfType<DialogueTrigger>().ToList();

            foreach (DialogueTrigger dialogue in dialogues)
            {
                if (dialogue.name == npcToDeliver.name)
                {
                    return dialogue;
                }
            }

            return npcToDeliver;
        }

        public override void _OnDisable()
        {
            _npcToDeliver.endedDialogue -= OnTalkToNpc;
        }

        private void OnTalkToNpc()
        {
            if (!PlayerInventory.Instance.HasItem(objectToDeliver)) return;
            PlayerInventory.Instance.RemoveItem(objectToDeliver);
            CompleteObjective();
        }
    }
}