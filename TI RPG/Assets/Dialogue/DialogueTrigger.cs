using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject player;
    public float distanciaMinima = 2f;
    public Dialogue dialogue;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void TriggerDialogue()
    {
        //if (!Input.GetKeyDown(KeyCode.F)) return;
        //float distancia = Vector3.Distance(transform.position, player.transform.position);
       // if (!(distancia <= distanciaMinima)) return;
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) // Check if the ray hits any collider
            {
                if (hit.collider.CompareTag("dialogavel")) // Check if the hit collider belongs to this object
                {
                    float distancia = Vector3.Distance(transform.position, player.transform.position);
                    if (!(distancia <= distanciaMinima)) return;
                    FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        TriggerDialogue();
    }

}
