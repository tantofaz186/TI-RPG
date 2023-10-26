using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace Rpg.Entities
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Outline))]
    public class Interactible : MonoBehaviour
    {
        public bool isMouseOver = false;
        public UnityEvent onClick = new UnityEvent();
        public UnityEvent onInteract = new UnityEvent();
        private static Camera mainCamera;
        private static PlayerMovement player;

        [Header("Raio de interação")] 
        
        [Range(0, 20)] [SerializeField] protected float distanciaMinima = 2f;
        [SerializeField] protected Color corDoGizmos = Color.yellow;
        private float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);

        [Header("Outline")] 
        [SerializeField] protected Color corDoOutline = Color.blue;
        [SerializeField] protected float larguraDoOutline = 4f;
        [SerializeField] protected Outline.Mode modoDoOutline = Outline.Mode.OutlineVisible;
        private Outline outline;

        public virtual void OnChangeIsMouseOver()
        {
            outline.enabled = isMouseOver;
        }

        private void OnMouseEnter()
        {
            isMouseOver = true;
            OnChangeIsMouseOver();
        }

        private void OnMouseExit()
        {
            isMouseOver = false;
            OnChangeIsMouseOver();
        }

        private void OnMouseDown()
        {
            onClick.Invoke();
            StopAllCoroutines();
            StartCoroutine(MoverParaObjeto());
        }
        private void OnValidate(){
            if (!TryGetComponent(out outline))
            {
                outline = gameObject.AddComponent<Outline>();
            }
            SetOutline(outline);
        }
        protected virtual void Awake()
        {
            if (mainCamera == null)
                mainCamera = Camera.main;
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

            if (!TryGetComponent(out outline))
            {
                outline = gameObject.AddComponent<Outline>();
            }
            SetOutline(outline);
        }

        private IEnumerator MoverParaObjeto()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input
                .mousePosition); // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
            if (!hit.collider.gameObject.Equals(gameObject))
                yield break; // Check if the hit collider belongs to this object
            if (player.isActiveAndEnabled) player.Mover(transform.position);
            while (distanciaDoPlayer > distanciaMinima) yield return null;
            onInteract?.Invoke();
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = corDoGizmos;
            Gizmos.DrawWireSphere(transform.position, distanciaMinima);
        }

        private void SetOutline(Outline outline)
        {
            outline.OutlineColor = corDoOutline;
            outline.OutlineMode = modoDoOutline;
            outline.OutlineWidth = larguraDoOutline;
            outline.enabled = false;
        }
    }
}