using System;
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
    }
}
