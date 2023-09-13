using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Player;


[System.Serializable]
public class ItemInventario : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string id;
    public string itemName;
    public Image icon;
    public Transform parentAfterDrag;
    public GameObject objeto;
    //private ItemInventario item;
    public bool isPicked = false;
    public bool taNoInventario = false;
    //[SerializeField] private bool quebrou = false;
    public int durabilidade;

    public InventoryManager manager;
    protected PlayerMovement player;
    protected float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);
    private Camera mainCamera;
    [SerializeField] protected float distanciaMinima = 2f;


    private void Awake()
    {
        //Debug.Log("Parent's name: " + transform.parent.name);
        parentAfterDrag = transform.parent;
        icon = GetComponent<Image>();
        
        //objeto = this.gameObject;
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
        if (manager==null)
        {
            manager = FindObjectOfType<InventoryManager>();
        }
        if (Input.GetKeyDown(KeyCode.I)&&isPicked==true&& taNoInventario==false)
        {
            ColocarInventario();
            isPicked = false;
            taNoInventario = true;
        }
        if (!Input.GetMouseButtonDown(0)&& isPicked == false) return;
        StopAllCoroutines();
        StartCoroutine(MoverParaObjeto());
    }
    private void ColocarInventario()
    {
        manager.AddItem(this);
        //depois de colocar o objeto aqui mandar ele pra narnia
        
        //Destroy(this.gameObject);
    }
    protected virtual IEnumerator MoverParaObjeto()
    {

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
        if (!hit.collider.gameObject.Equals(gameObject)) yield break; // Check if the hit collider belongs to this object
        if (player.isActiveAndEnabled) player.Mover(transform.position);
        while (distanciaDoPlayer > distanciaMinima) yield return null;
        PickUpObject();
    }
    private void PickUpObject()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();
        Collider pickupCollider = this.GetComponent<Collider>(); ;
        try
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, LayerMask.GetMask("Player"));

            foreach (Collider col in colliders)
            {
                Rigidbody colRb = col.GetComponent<Rigidbody>();

                if (colRb != null && colRb != rb && colRb.isKinematic)
                {
                    Debug.Log("O objeto pr�ximo n�o pode ser pego.");
                    return;
                }
            }

            rb.isKinematic = true;
            pickupCollider.enabled = false; // Disable the collider
            transform.parent = manager.mao.transform;
            transform.position = transform.parent.position;
            isPicked = true;
        }
        catch (UnityException)
        {
            Debug.Log("N�o h� nenhum objeto pr�ximo para ser pego");
        }

    }
}

