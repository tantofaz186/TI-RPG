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