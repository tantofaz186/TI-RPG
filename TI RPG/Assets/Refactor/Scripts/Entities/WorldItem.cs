using System;
using Rpg.Crafting;
using UnityEngine;

namespace Rpg.Entities
{
    public class WorldItem : Interactible
    {
        public Item item;
        
        [NonSerialized]
        public Rigidbody rigidbody;

        public void OnEnable()
        {
            UpdateDisplayedItem();
            rigidbody = GetComponent<Rigidbody>();
        }

        public void UpdateDisplayedItem()
        {
            if(transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);

            if(item != null)
                Instantiate(item.prefab, transform);
        }

        public void Collect()
        {
            if (PlayerInventory.Instance.AddItem(item))
            {
                Destroy(gameObject);
            }
        }
    }
}