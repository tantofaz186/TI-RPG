using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Rpg.Crafting
{
    public class InventorySlotEventHandler : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler,
        IDragHandler
    {
        public int slotId;

        public UnityEvent<int, Vector3> onClick = new();
        public UnityEvent<int, Vector3> onDown = new();
        public UnityEvent<int, Vector3> onUp = new();

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) onDown?.Invoke(slotId, eventData.pressPosition);
        }

        public void OnDrag(PointerEventData eventData) { }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            if (eventData.pointerCurrentRaycast.gameObject.Equals(eventData.lastPress)) return;
            if (eventData.pointerCurrentRaycast.gameObject.Equals(gameObject))
            {
                onUp?.Invoke(slotId, eventData.pressPosition);
            }
            else if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(
                         out InventorySlotEventHandler i))
            {
                i.OnEndDrag(eventData);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            onClick?.Invoke(slotId, eventData.pressPosition);
        }
    }
}