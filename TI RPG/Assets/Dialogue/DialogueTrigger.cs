using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public GameObject player;
    public float distanciaMinima = 2f;
    public Dialogue dialogue;
	float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);	
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Outline outline;
        if (TryGetComponent(out outline))
        {
            SetOutline(outline);
        }
        else
        {
            outline = gameObject.AddComponent<Outline>();
            SetOutline(outline);
        }
    }

    public void TriggerDialogue()
    {
        
        if (distanciaDoPlayer > distanciaMinima) return;
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) // Check if the ray hits any collider
        {
            if (hit.collider.gameObject.Equals(this.gameObject)) // Check if the hit collider belongs to this object
            {
                DialogueManager.Instance.StartDialogue(dialogue);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        TriggerDialogue();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaMinima);

    }

    private void SetOutline(Outline outline)
    {
        outline.OutlineColor = Color.yellow;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
    }
}
