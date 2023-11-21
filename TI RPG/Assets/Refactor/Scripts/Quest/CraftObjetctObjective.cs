using Rpg.Crafting;
using UnityEngine;

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest Objective", menuName = "RPG/Quests/Objectives/Craft Object")]
    public class CraftObjectObjective : QuestObjective
    {
        public Item objectToCraft;

        public void OnEnable()
        {
            PlayerInventory.Instance.onCraftItem += OnCraftItem;
        }

        public void OnDisable()
        {
            PlayerInventory.Instance.onCraftItem -= OnCraftItem;
        }

        private void OnCraftItem(Item craftedItem)
        {
            if (craftedItem.id == objectToCraft.id) CompleteObjective();
        }
    }
}