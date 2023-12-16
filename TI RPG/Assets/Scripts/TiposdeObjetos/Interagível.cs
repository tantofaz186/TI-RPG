using System.Collections;
using Player;
using UnityEngine;

namespace Objetos
{
    [RequireComponent(typeof(Outline))]
    public abstract class Interagível : MonoBehaviour
    {
        [Range(0, 20)]
        [SerializeField]
        protected float distanciaMinima = 2f;

        [SerializeField]
        protected Color corDoOutline = Color.blue;

        [SerializeField]
        protected float larguraDoOutline = 4f;

        [SerializeField]
        protected Outline.Mode modoDoOutline = Outline.Mode.OutlineVisible;

        [SerializeField]
        protected Color corDoGizmos = Color.yellow;

        protected Camera mainCamera;
        private Coroutine movingToObjeto;
        private Outline outline;
        protected PlayerMovement player;
        protected float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);

        protected virtual void Awake()
        {
            mainCamera = Camera.main;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
            if (!TryGetComponent(out outline)) outline = gameObject.AddComponent<Outline>();
            SetOutline(outline);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Gizmos.color = corDoGizmos;
            Gizmos.DrawWireSphere(transform.position, distanciaMinima);
        }

        private void OnMouseEnter()
        {
            outline.enabled = true;
        }

        private void OnMouseExit()
        {
            outline.enabled = false;
        }

        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (movingToObjeto != null) StopCoroutine(movingToObjeto);
            movingToObjeto = StartCoroutine(MoverParaObjeto());
        }

        private void OnValidate()
        {
            if (!TryGetComponent(out outline)) outline = gameObject.AddComponent<Outline>();
            SetOutline(outline);
        }

        protected virtual IEnumerator MoverParaObjeto()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input
                .mousePosition); // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
            if (!hit.collider.gameObject.Equals(gameObject))
            {
                yield break; // Check if the hit collider belongs to this object
            }

            if (player.isActiveAndEnabled) player.Mover(transform.position);
            yield return new WaitUntil(() => distanciaDoPlayer <= distanciaMinima);

            Interagir();
        }

        protected abstract void Interagir();

        protected virtual void SetOutline(Outline outline)
        {
            outline.OutlineColor = corDoOutline;
            outline.OutlineMode = modoDoOutline;
            outline.OutlineWidth = larguraDoOutline;
            outline.enabled = false;
        }
    }
}