using UnityEngine;

namespace Rpg.Crafting
{
    [CreateAssetMenu(fileName = "Item", menuName = "RPG/Inventory/Item", order = 100)]
    public class Item : ScriptableObject
    {
        public string id;
        public Sprite sprite;
        public GameObject prefab;

        public override string ToString()
        {
            return id;
        }
    }
}