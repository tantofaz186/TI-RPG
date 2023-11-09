using System;
using Rpg.Entities;
using Rpg.Interface;
using UnityEditor;
using UnityEngine;

namespace Rpg.Crafting
{
    [CreateAssetMenu(fileName = "Item", menuName = "RPG/Inventory/Item", order = 100)]
    public class Item : ScriptableObject
    {
        [Header("INFO")]
        public string id;
        
        [Header("DISPLAY")]
        public string displayName;
        public Sprite sprite;
        public GameObject prefab;
        public GameObject inspectPrefab;

        public override string ToString()
        {
            return id;
        }

#if UNITY_EDITOR
        [CustomEditor(typeof(Item))]
        public class ItemEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                // if (GUILayout.Button("Inspect Item"))
                // {
                //     Item item = (target as Item)!;
                //     item.InspectItem();
                // }
            }
        }
        #endif
    }
}