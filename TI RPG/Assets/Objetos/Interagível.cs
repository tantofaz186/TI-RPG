using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Objetos
{
    [RequireComponent(typeof(Outline))]
    public abstract class Interagível : MonoBehaviour
    {
        private Camera mainCamera;
        protected PlayerMovement player; 
        
        protected float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);	
        
        [SerializeField] protected float distanciaMinima = 2f;
        [SerializeField] protected Color corDoOutline = Color.blue;
        [SerializeField] protected float larguraDoOutline = 4f;
        [SerializeField] protected Outline.Mode modoDoOutline = Outline.Mode.OutlineVisible;
        [SerializeField] protected Color corDoGizmos = Color.yellow;
        Outline outline;
        void OnValidate(){
            if (!TryGetComponent(out outline))
            {
                outline = gameObject.AddComponent<Outline>();
            }
            SetOutline(outline);
        }
        protected virtual void Awake()
        {
            mainCamera = Camera.main;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if (!TryGetComponent(out outline))
            {
                outline = gameObject.AddComponent<Outline>();
            }
            SetOutline(outline);
        }
        void OnMouseEnter()
        {
            outline.enabled = true;
        }

        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            StopAllCoroutines();
            StartCoroutine(MoverParaObjeto());
        }

        void OnMouseExit()
        {
            outline.enabled = false;
        }

        protected virtual IEnumerator MoverParaObjeto()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
            if (!hit.collider.gameObject.Equals(gameObject)) yield break; // Check if the hit collider belongs to this object
            if(player.isActiveAndEnabled) player.Mover(transform.position);
            while (distanciaDoPlayer > distanciaMinima) yield return null;
            Interagir();
        }

        protected abstract void Interagir();
        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = corDoGizmos;
            Gizmos.DrawWireSphere(transform.position, distanciaMinima);
        }
        protected virtual void SetOutline(Outline outline)
        {
            outline.OutlineColor = corDoOutline;
            outline.OutlineMode = modoDoOutline;
            outline.OutlineWidth = larguraDoOutline;
        }
    }
}