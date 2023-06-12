using System.Collections;
using Player;
using UnityEngine;

namespace Objetos
{
    public class ObjetoEscondível : MonoBehaviour
    {
        public PlayerMovement player; 
        public float distanciaMinima = 2f; 
        private bool estaEscondido = false; 
        private Camera mainCamera;
        private float distanciaDoPlayer => Vector3.Distance(transform.position, player.transform.position);	

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

        void Update()
        {
            Esconder();
        }

        private IEnumerator MoverParaObjeto()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit)) yield break; // Check if the ray hits any collider
            if (!hit.collider.gameObject.Equals(gameObject)) yield break; // Check if the hit collider belongs to this object
            if(!estaEscondido) player.Mover(transform.position);
            while (distanciaDoPlayer > distanciaMinima) yield return null;
            player.gameObject.SetActive(estaEscondido);
            estaEscondido = !estaEscondido;
        }
        private void Esconder()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            StopAllCoroutines();
            StartCoroutine(MoverParaObjeto());

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, distanciaMinima);
        }
        private void SetOutline(Outline outline)
        {
            outline.OutlineColor = Color.blue;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineWidth = 6f;
        }
    }
}