using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    //public CraftingRecipeDatabase recipeDatabase; 

    public Slot[] inventorySlots;
    public string idCrafting1;
    public string idCrafting2;
    ItemInventario inventoryItem;
    public void CheckCrafting()
    {
       
    }
    public void AddItemCrafting()
    {
        for (int i=0; i<=inventorySlots.Length;i++)
        {
            if (inventorySlots[1].transform.childCount != 0)
            {
                inventoryItem = inventorySlots[i].GetComponent<ItemInventario>();
                idCrafting1 = inventoryItem.id;

            }
            else if (inventorySlots[2].transform.childCount != 0)
            {
                inventoryItem = inventorySlots[i].GetComponent<ItemInventario>();
                idCrafting2 = inventoryItem.id;

            }
        }
    }
}