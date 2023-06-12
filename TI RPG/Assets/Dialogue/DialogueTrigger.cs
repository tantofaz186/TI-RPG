using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public PlayerMovement player;
    public float distanciaMinima = 2f;
    public Dialogue dialogue;
    private Camera mainCamera;
	float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);	
    private void Awake()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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

    public IEnumerator TriggerDialogue()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
        if (!Physics.Raycast(ray, out var hit)) yield break; // Check if the ray hits any collider
        if (!hit.collider.gameObject.Equals(gameObject)) yield break; // Check if the hit collider belongs to this object
        player.Mover(transform.position);
        while (distanciaDoPlayer > distanciaMinima) yield return null;
        player.Mover(player.transform.position);
        DialogueManager.Instance.StartDialogue(dialogue);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        StopAllCoroutines();
        StartCoroutine(TriggerDialogue());
        
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
