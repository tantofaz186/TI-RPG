using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class ItemInventario : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string id;
    public string itemName;
    public Image icon;
    public Transform parentAfterDrag;
    public GameObject objeto;
    public ItemInventario item;


    public void InitializeItem(ItemInventario newItem)
    {
        item = newItem;
        icon = newItem.icon;
    }

    private void Awake()
    {
        //Debug.Log("Parent's name: " + transform.parent.name);
        parentAfterDrag = transform.parent;
        icon = GetComponent<Image>();
        objeto = this.gameObject;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        icon.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        icon.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
   
}
