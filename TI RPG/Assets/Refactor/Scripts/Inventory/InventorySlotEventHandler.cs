using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rpg.Crafting
{
    public class InventorySlotEventHandler : MonoBehaviour, IPointerClickHandler
    {
        public int slotId;

        public UnityEvent<int> onClick = new UnityEvent<int>();
        
        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(slotId);
        }
    }
}