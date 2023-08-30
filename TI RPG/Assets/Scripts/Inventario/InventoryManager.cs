using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    private InventorySlot slot;
    private ItemInventario itemInSlot;
    public GameObject inventoryItemPrefab;
    private ItemInventario itemInventarioAuxSpawn;
    int selectedSlot= -1;
    private void Start()
    {
        changeSelectedSlot(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            changeSelectedSlot(0);
        }
        if (Input.GetKeyDown("2"))
        {
            changeSelectedSlot(1);
        }
        if (Input.GetKeyDown("3"))
        {
            changeSelectedSlot(2);
        }
    }
    void changeSelectedSlot(int newValue)
    {
        if (selectedSlot>=0)
        {
        inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }
     public bool AddItem(ItemInventario item)
    {
        for (int i=0; i<inventorySlots.Length; i++)
        {
            slot = inventorySlots[i];
            item = slot.GetComponentInChildren<ItemInventario>();
            if (itemInSlot==null)
            {
                SpawnItem(item, slot);
                return true;
            }
        }
        return false;
    }
    public void SpawnItem(ItemInventario item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab,slot.transform);
        itemInventarioAuxSpawn = newItemGo.GetComponent<ItemInventario>();
        itemInventarioAuxSpawn.InitializeItem(item);
    }
    public ItemInventario GetSelectedSlot(bool foiUsado)
    {
        InventorySlot invSlot = inventorySlots[selectedSlot];
        itemInSlot = slot.GetComponentInChildren<ItemInventario>();
        if (itemInSlot == null)
        {
            ItemInventario item= itemInSlot.item;
            if (foiUsado==true)
            {
                Destroy(itemInSlot.gameObject);
            }
            return item;
        }
        return null;
    }
}
