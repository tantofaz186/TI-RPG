using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    private InventorySlot slot;
    private ItemInventario itemNoSlot;
    public GameObject inventoryItemActive;
    [SerializeField] private PlayerMovement player;
    public GameObject mao;
    [SerializeField] public string maoNome;
    int selectedSlot= -1;
    private ItemInventario itemInventarioAuxSpawn;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //changeSelectedSlot(0);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        try
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            mao = EncontrarMao(player.gameObject, maoNome);
        }
        catch
        {
            // ignored
        }
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            //UseSelectedItem();
        }
    }
    void changeSelectedSlot(int newValue)
    {
        if (selectedSlot>=0)
        {
            inventorySlots[selectedSlot].Deselect();
        }
        inventorySlots[newValue].Select();
        //Debug.Log(inventorySlots[newValue].transform.GetChild(0).name);
        selectedSlot = newValue;
        SpawnSelectedItem(newValue);
    }
    GameObject EncontrarMao(GameObject _player, string nome)
    {
        for (int i = 0; i < _player.transform.childCount; i++)
        {
            if (_player.transform.GetChild(i).name == nome)
            {
                return _player.transform.GetChild(i).gameObject;
            }

            GameObject aux = EncontrarMao(_player.transform.GetChild(i).gameObject, maoNome);

            if (aux != null)
            {
                return aux;
            }
        }
        return null;
    }
    public void SpawnSelectedItem(int i)
    {
        Destroy(inventoryItemActive);
        if (selectedSlot >= 0)
        {
            InventorySlot selectedInventorySlot = inventorySlots[selectedSlot];
            ItemInventario itemInSlot = selectedInventorySlot.GetComponentInChildren<ItemInventario>();
            inventoryItemActive = itemInSlot.objeto;
            Rigidbody rb = inventoryItemActive.GetComponent<Rigidbody>();
            Collider pickupCollider= inventoryItemActive.GetComponent<Collider>(); ;
            if (inventoryItemActive != null)
            {
                if (rb!=null)
                {
                    rb.isKinematic = true;
                }
                if(pickupCollider!=null)
                {
                    pickupCollider.enabled = false; // Disable the collider
                }
                inventoryItemActive = Instantiate(inventoryItemActive, mao.transform);
                //transform.parent = mao.transform;
                transform.position = transform.parent.position;
                Debug.Log("Spawned item: " + inventoryItemActive.gameObject.name);
            }
        }
    }
    public bool AddItem(ItemInventario item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            slot = inventorySlots[i];
            itemNoSlot = slot.GetComponentInChildren<ItemInventario>();
            if (itemNoSlot == null)
            {
                SpawnItemInventario(item, slot);

                return true; 
            }
        }
        return false;
    }

    public void SpawnItemInventario(ItemInventario item, InventorySlot slot)
    {

        GameObject newItemGo = Instantiate(item.gameObject, slot.transform);
        itemInventarioAuxSpawn = newItemGo.GetComponent<ItemInventario>();
        itemInventarioAuxSpawn.InitializeItem(item);
    }
}