using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rpg.Crafting
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "RPG/Inventory/Recipe", order = 100)]
    public class Recipe : ScriptableObject
    {
        public Item[] ingredients;
        public Item result;

        public override string ToString()
        {
            return $"{String.Join(' ', (object[]) ingredients)} => {result}";
        }
    }

    public class RecipeInfo
    {
        public Recipe recipe;
        public bool isCraftable => recipe.ingredients.Length == foundItems;
        public int foundItems;

        public override string ToString()
        {
            return $"{recipe} ({foundItems} = {isCraftable})";
        }

        public bool Matches(Item[] items)
        {
            List<Item> tempItems = new List<Item>();
            
            tempItems.AddRange(items.Where(i => i != null));

            foreach (var ign in recipe.ingredients)
            {
                int idx = tempItems.IndexOf(ign);
                if (idx == -1)
                    return false;
                
                tempItems.RemoveAt(idx);
            }

            return tempItems.Count == 0;
        }
    }
}
