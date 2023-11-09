using System;
using System.Collections;
using Rpg.Crafting;
using Rpg.Interface;
using UnityEngine;

namespace Rpg.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    public class WorldItem : Interactible
    {
        public Item item;
        public bool canBeCollected;
        [NonSerialized] public Rigidbody rigidbody;

        public void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            onInteract.AddListener(InspectItem);
        }

        private void OnDisable()
        {
            onInteract.RemoveListener(InspectItem);
        }

        public void UpdateDisplayedItem()
        {
            if (transform.childCount > 0)
                Destroy(transform.GetChild(0).gameObject);

            if (item != null)
                Instantiate(item.prefab, transform);
        }

        public void InspectItem()
        {
            InspectScreen.Instance.InspectItem(this);
        }

        public bool Collect()
        {
            if (!canBeCollected) return false;
            return PlayerInventory.Instance.AddItem(item);
        }
    }
}