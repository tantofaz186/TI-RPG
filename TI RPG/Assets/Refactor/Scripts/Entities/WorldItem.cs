using System;
using System.Collections;
using Rpg.Crafting;
using Rpg.Interface;
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
            onInteract.AddListener(item.InspectItem);
        }

        private void OnDisable()
        {
            onInteract.RemoveListener(item.InspectItem);
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