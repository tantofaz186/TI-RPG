using UnityEngine;

namespace Objetos
{
    public class ObjetoEscondível : MonoBehaviour
    {
        public GameObject player; 
        public float distanciaMinima = 2f; 
        private bool estaEscondido = false; 

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

        void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E)) return;
            // Verifica se o jogador está perto o suficiente do objeto para se esconder
            float distancia = Vector3.Distance(transform.position, player.transform.position);
            if (!(distancia <= distanciaMinima)) return;
            player.SetActive(estaEscondido); 
            estaEscondido = !estaEscondido;

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