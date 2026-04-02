using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemActive;

    [SerializeField]
    private PlayerMovement player;

    public GameObject mao;

    [SerializeField]
    public string maoNome;

    private Image imagemItemNoSlot;
    private ItemInventario itemInventarioAuxSpawn;
    private ItemInventario itemNoSlot;
    private int selectedSlot = -1;
    private InventorySlot slot;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        //changeSelectedSlot(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown("1")) changeSelectedSlot(0);
        if (Input.GetKeyDown("2")) changeSelectedSlot(1);
        if (Input.GetKeyDown("3")) changeSelectedSlot(2);
        if (Input.GetKeyDown(KeyCode.E))
        {
            //UseSelectedItem();
        }
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

    private void changeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0) inventorySlots[selectedSlot].Deselect();
        inventorySlots[newValue].Select();
        //Debug.Log(inventorySlots[newValue].transform.GetChild(0).name);
        selectedSlot = newValue;
        SpawnSelectedItem(newValue);
    }

    private GameObject EncontrarMao(GameObject _player, string nome)
    {
        for (int i = 0; i < _player.transform.childCount; i++)
        {
            if (_player.transform.GetChild(i).name == nome) return _player.transform.GetChild(i).gameObject;

            GameObject aux = EncontrarMao(_player.transform.GetChild(i).gameObject, maoNome);

            if (aux != null) return aux;
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
            Collider pickupCollider = inventoryItemActive.GetComponent<Collider>();
            ;
            if (inventoryItemActive != null)
            {
                if (rb != null) rb.isKinematic = true;
                if (pickupCollider != null) pickupCollider.enabled = false; // Disable the collider
                inventoryItemActive = Instantiate(inventoryItemActive, mao.transform);
                //transform.parent = mao.transform;
                transform.position = transform.parent.position;
                //Debug.Log("Spawned item: " + inventoryItemActive.gameObject.name);
            }
        }
    }

    public bool AddItem(ItemInventario item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            slot = inventorySlots[i];
            itemNoSlot = slot.GetComponentInChildren<ItemInventario>();
            //imagemItemNoSlot=;

            if (itemNoSlot.id == null || (itemNoSlot.id == "" && item.taNoInventario == false && item.isPicked))
            {
                slot.GetComponentInChildren<ItemInventario>().id = item.id;
                slot.GetComponentInChildren<ItemInventario>().itemName = item.itemName;
                slot.GetComponentInChildren<ItemInventario>().icon = item.icon;
                slot.GetComponentInChildren<ItemInventario>().objeto = item.objeto;
                slot.GetComponentInChildren<ItemInventario>().isPicked = false;
                slot.GetComponentInChildren<ItemInventario>().taNoInventario = true;
                slot.GetComponentInChildren<ItemInventario>().durabilidade = item.durabilidade;
                slot.GetComponentInChildren<Image>().sprite = item.icon.sprite;
                //SpawnItemInventario(item, slot);
                //itemNoSlot=item;
                //UnityEngine.Debug.Log(itemNoSlot.itemName);
                //imagemItemNoSlot=item.icon;
                //slot.GetComponentInChildren<Image>() = item.icon;
                //slot.GetComponentInChildren<ItemInventario>().ItemInventario = item;
                //itemInventarioAuxSpawn.InitializeItem(item);
                item.taNoInventario = true;
                return true;
            }
        }

        return false;
    }
}