using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : MonoBehaviour
{
    //public CraftingRecipeDatabase recipeDatabase; 

    public InventorySlot[] inventorySlots;
    public string idCrafting1;
    public string idCrafting2;
    public ItemInventario prefabResultLanterna;
    private ItemInventario inventoryItem;

    public void AddItemCrafting()
    {
        if (inventorySlots[0].transform.childCount > 0)
        {
             inventoryItem = inventorySlots[0].transform.GetComponentInChildren<ItemInventario>();
            idCrafting1 = inventoryItem.id;
            //Debug.Log(inventoryItem.gameObject.name);
        }
        if (inventorySlots[1].transform.childCount > 0)
        {
             inventoryItem = inventorySlots[1].transform.GetComponentInChildren<ItemInventario>();
            idCrafting2 = inventoryItem.id;
            //Debug.Log(inventoryItem.gameObject.name);
        }
        ChecarCrafting();
    }
    public void ChecarCrafting()
    {
        if (inventorySlots[0].transform.childCount > 0 && inventorySlots[1].transform.childCount > 0)
        {
            ReceitasCrafting();
        }
    }
    public void ReceitasCrafting()
    {
        if (idCrafting1.Equals("1") && idCrafting2.Equals("2"))
        {
            GameObject resultObject = Instantiate(prefabResultLanterna.gameObject);

            //resultObject.transform.localScale = new Vector3(7436601f, 7436601f, 7436601f);
            resultObject.transform.SetParent(inventorySlots[2].transform);
            //idCrafting1 = "";
            //idCrafting2 = "";
        }
    }
    private void FixedUpdate()
    {
        if (inventorySlots[0].transform.childCount == 0)
        {
            idCrafting1 = "";
        }else if (inventorySlots[1].transform.childCount == 0)
        {
            idCrafting2 = "";
        }
    }
}