using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Rpg.Entities;
using Rpg.Save;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rpg.Crafting
{
    public class PlayerInventory : Singleton<PlayerInventory>
    {
        public Action hasChangedItems;

        [Header("DRAGGING")]
        public Item draggedItem;

        public Vector2 draggingOffset;
        public Image draggedItemDisplay;

        [Header("REFERENCES")]
        public Recipe[] recipes;

        public Item[] items;
        public Image[] slots;
        public GameObject prefabWorldItem;

        [Header("STATE")]
        [SerializeField]
        private Item[] contents = new Item[26];

        public int firstContainerSlot = 4;
        public bool shouldIgnoreCrafting;
        public event Action<Item> onCraftItem;

        #region Callbacks

        private void OnEnable()
        {
            SaveManager.mustReloadData += OnReloadContents;
            hasChangedItems += OnChangedContents;
            OnReloadContents();

            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlotEventHandler comp = slots[i].gameObject.AddComponent<InventorySlotEventHandler>();
                comp.slotId = i;
                comp.onClick.AddListener(OnClickOnSlot);
                comp.onUp.AddListener(OnClickOnSlot);
                comp.onDown.AddListener(OnClickOnSlot);
            }
        }

        private void OnDisable()
        {
            SaveManager.mustReloadData -= OnReloadContents;
            hasChangedItems -= OnChangedContents;

            foreach (Image image in slots)
                if (image != null)
                    Destroy(image.gameObject.GetComponent<InventorySlotEventHandler>());
        }

        private void Update()
        {
            if (draggedItem == null)
                return;

            Vector2 mp = Input.mousePosition;
            draggedItemDisplay.rectTransform.position = mp - draggingOffset;
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

            RefreshCrafting();
            RefreshVisual();
        }

        public void OnChangedContents()
        {
            string[] itemIds = new string[contents.Length];

            for (int i = 0; i < contents.Length; i++)
            {
                if (contents[i] == null)
                    continue;
                itemIds[i] = contents[i].id;
            }

            RefreshCrafting();
            SaveManager.Instance.data.playerInventory = itemIds;
            RefreshVisual();
        }

        public void RefreshVisual()
        {
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

        public void RefreshCrafting()
        {
            if (shouldIgnoreCrafting)
            {
                shouldIgnoreCrafting = false;
                return;
            }

            shouldIgnoreCrafting = true;
            Item[] ingredients = new Item[3];
            Array.Copy(contents, ingredients, 3);
            foreach (RecipeInfo recipe in GetAvailableRecipes())
                if (recipe.Matches(ingredients))
                {
                    SetItem(firstContainerSlot - 1, recipe.recipe.result);
                    onCraftItem?.Invoke(recipe.recipe.result);
                    return;
                }

            SetItem(firstContainerSlot - 1, null);
        }

        public void OnClickOnSlot(int slot)
        {
            if (slot >= 0)
            {
                if (draggedItem == null)
                {
                    Item item = contents[slot];
                    if (item != null)
                    {
                        if (slot == firstContainerSlot - 1)
                            for (int i = 0; i < firstContainerSlot - 1; i++)
                                contents[i] = null;

                        //Grab item
                        SetItem(slot, null);
                        SetDraggedItem(item);
                        draggingOffset = Input.mousePosition - slots[slot].rectTransform.position;
                    }
                }
                else
                {
                    Item item = contents[slot];
                    if (item == null && slot != firstContainerSlot - 1)
                    {
                        SetItem(slot, draggedItem);
                        SetDraggedItem(null);
                    }
                }
            }
            else
            {
                if (draggedItem == null)
                    return;

                DropItem(draggedItem);
                SetDraggedItem(null);
            }
        }

        #endregion

        #region Inventory Methods

        public bool HasItem(Item item)
        {
            return Array.IndexOf(contents, item) >= firstContainerSlot;
        }

        public int GetItemCount(Item item)
        {
            return contents.Select((s, i) => i >= firstContainerSlot && s != null && s == item).Count();
        }

        public bool HasEmptySlot()
        {
            return FirstEmptySlot() >= 0;
        }

        public int FirstEmptySlot()
        {
            for (int i = firstContainerSlot; i < contents.Length; i++)
                if (contents[i] == null)
                    return i;
            return -1;
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
            for (int i = firstContainerSlot; i < contents.Length; i++)
                if (contents[i] != null && contents[i] == item)
                {
                    contents[i] = null;
                    hasChangedItems?.Invoke();
                    return true;
                }

            return false;
        }

        public void SetItem(int slot, Item item)
        {
            contents[slot] = item;
            hasChangedItems?.Invoke();
        }

        public Item GetItem(int slot)
        {
            return contents[slot];
        }

        public IEnumerable<Item> GetItems()
        {
            return contents;
        }

        public void SetDraggedItem(Item item)
        {
            draggedItem = item;
            draggingOffset = Vector2.zero;
            draggedItemDisplay.sprite = item == null ? null : item.sprite;
            draggedItemDisplay.gameObject.SetActive(item != null);
        }

        #endregion

        #region Inventory Utils

        public Item GetItemById(string id)
        {
            return items.FirstOrDefault(i => i.id == id);
        }

        public IEnumerable<RecipeInfo> GetAvailableRecipes()
        {
            List<RecipeInfo> infos = new();

            foreach (Recipe recipe in recipes)
                infos.Add(new RecipeInfo
                {
                    recipe = recipe,
                    foundItems = 0
                });

            foreach (Item item in contents)
            foreach (RecipeInfo recipeInfo in infos)
            {
                if (recipeInfo.isCraftable)
                    continue;
                if (recipeInfo.recipe.ingredients.Contains(item))
                    recipeInfo.foundItems++;
            }

            foreach (RecipeInfo recipeInfo in infos)
                if (recipeInfo.foundItems > 0)
                    yield return recipeInfo;
        }

        public void DropItem(Item item)
        {
            WorldItem go = Instantiate(prefabWorldItem).GetComponent<WorldItem>();
            go.item = item;
            go.UpdateDisplayedItem();
            Vector3 v = Random.onUnitSphere;
            Vector3 dir = new Vector3(v.x, 0, v.z).normalized;
            go.transform.position = transform.position;
            go.rigidbody.velocity = dir * 2 + Vector3.up * 2;
            go.rigidbody.angularVelocity =
                new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
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