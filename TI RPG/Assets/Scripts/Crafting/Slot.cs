using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
{
    public bool craftResultSlot;
    //public Image slotImage;
    public Color selected = new Color(0, 1, 1, 1);
    public Color notSelected = new Color(0.451f, 0.451f, 0.451f, 1);
    public int idCrafting;

    CraftingManager manager;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 && craftResultSlot == false)
        {
            ItemInventario inventoryItem = eventData.pointerDrag.GetComponent<ItemInventario>();
            inventoryItem.transform.SetParent(transform);
            inventoryItem.parentAfterDrag = transform;
            manager.AddItemCrafting();
        }
        Debug.Log("ola");
    }
}
