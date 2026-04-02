using Rpg.Crafting;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest Objective", menuName = "RPG/Quests/Objectives/Craft Object")]
    public class CraftObjectObjective : QuestObjective
    {
        public Item objectToCraft;

        public override void _OnEnable()
        {
            Debug.Log("Craft Object Objective Enabled");
            PlayerInventory.Instance.onCraftItem += OnCraftItem;
            if (dialogueOnStart != null && dialogueOnStart.dialogues.Length > 0)
            {
                DialogueManager.Instance.StartDialogue(dialogueOnStart);
            }
        }

        public override void _OnDisable()
        {
            Debug.Log("Craft Object Objective Disabled");
            PlayerInventory.Instance.onCraftItem -= OnCraftItem;
        }

        private void OnCraftItem(Item craftedItem)
        {
            if (craftedItem.id == objectToCraft.id) CompleteObjective();
        }
    }
}