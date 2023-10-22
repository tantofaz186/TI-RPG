using System;
using UnityEditor;
using UnityEngine;

namespace Rpg.Crafting
{
    [CreateAssetMenu(fileName = "Item", menuName = "RPG/Inventory/Item", order = 100)]
    public class Item : ScriptableObject
    {
        public string id;
        public Sprite sprite;
        public GameObject prefab;
        public static Action<Mesh> onItemInspect;

        public override string ToString()
        {
            return id;
        }

        public virtual void InspectItem()
        {
            onItemInspect?.Invoke(prefab.GetComponent<MeshFilter>().sharedMesh);
        }

        #if UNITY_EDITOR
        [CustomEditor(typeof(Item))]
        public class ItemEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                if (GUILayout.Button("Inspect Item"))
                {
                    Item item = (target as Item)!;
                    item.InspectItem();
                }
            }
        }
        #endif
    }
}