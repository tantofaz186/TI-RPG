using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Crafting
{
    public class SlotInv : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public ItemInv Item
        {
            get => item;
            private set
            {
                item = value;
                image.sprite = value.IsUnityNull()? null : item.sprite;
            }
        }

        [SerializeField] private Image image;
        [SerializeField] private ItemInv item;
        Vector3 positionBeforeDrag;

        private void Awake()
        {
            image = GetComponent<Image>();
            positionBeforeDrag = transform.position;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            image.raycastTarget = false;
            positionBeforeDrag = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            image.raycastTarget = true;
            transform.position = positionBeforeDrag;
        }

        public void OnDrop(PointerEventData eventData)
        {
            var droppedItemSlot = eventData.pointerDrag.GetComponent<SlotInv>();
            if (addItem(droppedItemSlot.Item)) droppedItemSlot.removeItem();
            
        }

        bool isEmpty()
        {
            return Item.IsUnityNull();
        }

        public bool addItem(ItemInv item)
        {
            if (!isEmpty()) return false;
            Item = item;
            return true;
        }

        public bool removeItem()
        {
            if (isEmpty()) return false;
            Item = null;
            return true;
        }
    }
}