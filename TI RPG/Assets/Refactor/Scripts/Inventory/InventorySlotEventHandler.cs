using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rpg.Crafting
{
    public class InventorySlotEventHandler : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public int slotId;

        public UnityEvent<int> onClick = new();
        public UnityEvent<int> onDown = new();
        public UnityEvent<int> onUp = new();

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(slotId);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) onDown?.Invoke(slotId);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) onUp?.Invoke(slotId);
        }
    }
}