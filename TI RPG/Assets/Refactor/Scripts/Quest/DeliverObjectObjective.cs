using Rpg.Crafting;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest Objective", menuName = "RPG/Quests/Objectives/Deliver Object")]
    public class DeliverObjectObjective : QuestObjective
    {
        public Item objectToDeliver;
        public DialogueTrigger npcToDeliver;

        public void OnEnable()
        {
            npcToDeliver.endedDialogue += OnTalkToNpc;
        }

        public void OnDisable()
        {
            npcToDeliver.endedDialogue -= OnTalkToNpc;
        }

        private void OnTalkToNpc()
        {
            if (PlayerInventory.Instance.HasItem(objectToDeliver)) CompleteObjective();
        }
    }
}