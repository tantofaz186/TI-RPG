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
    //private ItemInventario item;
    public InventoryManager manager;
    [SerializeField] private bool isPicked = false;
    public bool taNoInventario = false;
    [SerializeField] private bool quebrou= false;
    [SerializeField] private int durabilidade;

    public void InitializeItem(ItemInventario newItem)
    {
        //item = newItem;
        id = newItem.id;
        itemName = newItem.itemName;
        icon = newItem.icon;
        isPicked = newItem.isPicked;
        quebrou = newItem.quebrou;
        durabilidade = newItem.durabilidade;
        objeto = newItem.objeto;

    }

    private void Awake()
    {
        //Debug.Log("Parent's name: " + transform.parent.name);
        parentAfterDrag = transform.parent;
        icon = GetComponent<Image>();
        manager = FindObjectOfType<InventoryManager>();
        //objeto = this.gameObject;
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ColocarInventario();
        }
    }
    private void ColocarInventario()
    {
        manager.AddItem(this);
        //Destroy(gameObject);
    }

}

