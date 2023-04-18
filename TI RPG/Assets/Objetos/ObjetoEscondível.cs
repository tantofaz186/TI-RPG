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
    }
}