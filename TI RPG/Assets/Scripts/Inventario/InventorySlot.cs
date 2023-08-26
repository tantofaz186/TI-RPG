using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public bool craftResultSlot;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && craftResultSlot==false)
        {
            ItemInventario inventoryItem = eventData.pointerDrag.GetComponent<ItemInventario>();
            inventoryItem.parentAfterDrag = transform;
            inventoryItem.transform.SetParent(transform);
        }
            Debug.Log("ola");
    }
}
