using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Crafting
{
    public class ItemInv: MonoBehaviour
    {
        public Sprite sprite;
        private DialogueTrigger dialogueTrigger;

        private void Awake()
        {
            dialogueTrigger = GetComponent<DialogueTrigger>();
            dialogueTrigger.endedDialogue = addItemToInventory;
        }

        private void addItemToInventory()
        {
            CraftManager.Instance.addItem(this);
            dialogueTrigger.gameObject.SetActive(false);
        }
    }
}