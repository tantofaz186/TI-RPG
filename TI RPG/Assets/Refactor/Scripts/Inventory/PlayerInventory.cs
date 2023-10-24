using System;
using System.Collections.Generic;
using System.Linq;
using Rpg.Save;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rpg.Crafting
{
    public class PlayerInventory : MonoBehaviour
    {
        public Action hasChangedItems;

        [Header("REFERENCES")] 
        public Recipe[] recipes;
        public Item[] items;
        public Image[] slots;

        [Header("STATE")] [SerializeField] private Item[] contents = new Item[23];
        public int firstContainerSlot = 3;

        #region Callbacks
        private void OnEnable()
        {
            SaveManager.mustReloadData += OnReloadContents;
            hasChangedItems += OnChangedContents;
            OnReloadContents();
        }

        private void OnDisable()
        {
            SaveManager.mustReloadData -= OnReloadContents;
            hasChangedItems -= OnChangedContents;
        }

        public void OnReloadContents()
        {
            string[] itemIds = SaveManager.Instance.data.playerInventory;

            if (itemIds == null || itemIds.Length != contents.Length)
            {
                OnChangedContents();
                return;
            }

            contents = new Item[contents.Length];
            for (int i = 0; i < itemIds.Length; i++)
                contents[i] = GetItemById(itemIds[i]);

            RefreshVisual();
        }

        public void OnChangedContents()
        {
            string[] itemIds = new String[contents.Length];

            for (int i = 0; i < itemIds.Length; i++)
            {
                if (contents[i] == null)
                    continue;
                itemIds[i] = contents[i].id;
            }

            SaveManager.Instance.data.playerInventory = itemIds;
            RefreshVisual();
        }

        public void RefreshVisual()
        {
            // foreach (var recipe in GetAvailableRecipes())
            // {
            //     Debug.Log(recipe);
            // }

            for (int i = 0; i < contents.Length; i++)
            {
                Item item = contents[i];
                if (item == null)
                {
                    slots[i].sprite = null;
                    continue;
                }
                slots[i].sprite = item.sprite;
            }
        }
        #endregion

        #region Inventory Methods

        public bool HasItem(Item item)
        {
            return Array.IndexOf(contents, item) >= 0;
        }

        public int GetItemCount(Item item)
        {
            return contents.Select(s => s != null && s == item).Count();
        }

        public bool HasEmptySlot()
        {
            return FirstEmptySlot() >= 0;
        }

        public int FirstEmptySlot()
        {
            return Array.IndexOf(contents, null);
        }

        public void Clear()
        {
            Array.Fill(contents, null);
            hasChangedItems?.Invoke();
        }

        public bool AddItem(Item item)
        {
            int slot = FirstEmptySlot();
            if (slot == -1)
                return false;

            contents[slot] = item;
            hasChangedItems?.Invoke();
            return true;
        }

        public bool RemoveItem(Item item)
        {
            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i] != null && contents[i] == item)
                {
                    contents[i] = null;
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<Item> GetItems()
        {
            return contents;
        }

        #endregion

        #region Inventory Utils

        public Item GetItemById(string id)
        {
            return items.FirstOrDefault(i => i.id == id);
        }

        public IEnumerable<RecipeInfo> GetAvailableRecipes()
        {
            List<RecipeInfo> infos = new List<RecipeInfo>();

            foreach (Recipe recipe in recipes)
            {
                infos.Add(new RecipeInfo()
                {
                    recipe = recipe,
                    foundItems = 0
                });
            }

            foreach (Item item in contents)
            {
                foreach (RecipeInfo recipeInfo in infos)
                {
                    if(recipeInfo.isCraftable)
                        continue;
                    if(recipeInfo.recipe.ingredients.Contains(item))
                        recipeInfo.foundItems++;
                }
            }

            foreach (RecipeInfo recipeInfo in infos)
                if (recipeInfo.foundItems > 0)
                    yield return recipeInfo;
        }

        #endregion
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(PlayerInventory))]
    public class PlayerInventoryEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Auto Register"))
            {
                PlayerInventory inventory = (target as PlayerInventory)!;
                inventory.items = GetAllInstances<Item>().ToArray();
                inventory.recipes = GetAllInstances<Recipe>().ToArray();
            }
            
            if (GUILayout.Button("Refresh Contents"))
            {
                PlayerInventory inventory = (target as PlayerInventory)!;
                inventory.OnChangedContents();
            }
            
            if (GUILayout.Button("Clear"))
            {
                PlayerInventory inventory = (target as PlayerInventory)!;
                inventory.Clear();
            }
        }
        
        public IEnumerable<T> GetAllInstances<T>() where T : ScriptableObject
        {
            return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>)
                .ToList();
        }
    }
    #endif
}