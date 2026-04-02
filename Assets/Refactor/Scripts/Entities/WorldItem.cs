using System;
using Rpg.Crafting;
using Rpg.Interface;
using UnityEngine;

namespace Rpg.Entities
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(DialogueTrigger))]
    public class WorldItem : MonoBehaviour
    {
        public Item item;
        public bool canBeCollected;

        [SerializeField]
        private DialogueTrigger dialogueTrigger;

        [NonSerialized]
        public Rigidbody rigidbody;

        public void OnEnable()
        {
            rigidbody = GetComponent<Rigidbody>();
            dialogueTrigger = GetComponent<DialogueTrigger>();
            rigidbody.isKinematic = true;
            dialogueTrigger.endedDialogue += InspectItem;
        }

        private void OnDisable()
        {
            dialogueTrigger.endedDialogue -= InspectItem;
        }

        public void UpdateDisplayedItem()
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            if (item != null)
            {
                Instantiate(item.prefab, transform);
            }
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