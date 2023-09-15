using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class Coletavel : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private GameObject mao;
    [SerializeField] public string maoNome;
    public float distanciaMinima = 2f;
    bool carregada = false;
    private Rigidbody rb;
    private Camera mainCamera;
    float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);
    [SerializeField] protected float larguraDoOutline = 4f;
    [SerializeField] protected Outline.Mode modoDoOutline = Outline.Mode.OutlineVisible;
    [SerializeField] protected Color corDoOutline = Color.green;
<<<<<<< HEAD

=======
    private Collider collider;
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
    public bool Carregada
    {
        get { return carregada; }
    }

    void Pegar()
    {
        carregada = true;
        transform.parent = mao.transform;
        transform.position = transform.parent.position;
        rb.isKinematic = true;
<<<<<<< HEAD
        Collider collider = GetComponent<Collider>();
=======
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
        if (collider != null)
        {
            collider.enabled = false; // Disable the collider when picked up
        }
        Debug.Log("pegou");
    }

    void Largar()
    {
        transform.parent = null;
        rb.isKinematic = false;
        carregada = false;
<<<<<<< HEAD
        Collider collider = GetComponent<Collider>();
=======
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
        if (collider != null)
        {
            collider.enabled = true; // Enable the collider when dropped
        }
    }

    GameObject EncontrarMao(GameObject _player, string nome)
    {
        for (int i = 0; i < (_player.transform.childCount); i++)
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

    private Outline outline;
    void OnMouseEnter()
    {
        outline.enabled = true;
    }

    void OnMouseExit()
    {
        outline.enabled = false;
    }
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        mao = EncontrarMao(player.gameObject, maoNome);
        mainCamera = Camera.main;
<<<<<<< HEAD

=======
        collider = GetComponent<Collider>();
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
        outline = gameObject.GetComponent<Outline>();
        if (outline == null)
        {
            outline = gameObject.AddComponent<Outline>();
        }
<<<<<<< HEAD

=======
        
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
        SetOutline(outline);
    }

    public IEnumerator IrAtéObjeto()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
        if (!hit.collider.gameObject.Equals(gameObject)) yield break; // Check if the hit collider belongs to this object
        player.Mover(transform.position);
        while (distanciaDoPlayer > distanciaMinima) yield return null;
        Pegar();
    }

    protected virtual void SetOutline(Outline outline)
    {
        outline.OutlineColor = corDoOutline;
        outline.OutlineMode = modoDoOutline;
        outline.OutlineWidth = larguraDoOutline;
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        StopAllCoroutines();
        if (!carregada)
        {
            StartCoroutine(IrAtéObjeto());
        }
        else
        {
            Largar();
        }
    }
}
