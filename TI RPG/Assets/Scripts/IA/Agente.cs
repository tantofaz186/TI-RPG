using System.Collections;
using Objetos;
using UnityEngine;
using UnityEngine.AI;

namespace IA
{
    public class Agente : StateMachine
    {
        [SerializeField]
        protected float velocidade = 5f;

        [SerializeField]
        protected NavMeshAgent agente;

        [SerializeField]
        protected float audioDetectionRadius = 10f;

        [SerializeField]
        protected Animator animator;

        public Animator Animator => animator;

        private void Awake()
        {
            agente = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            agente.speed = velocidade;
        }

        public void Mover(Vector3 ponto)
        {
            agente.SetDestination(ponto);
        }

        public IEnumerator GetSpeedBoost(float time, float multiplier)
        {
            agente.speed = velocidade * multiplier;
            yield return new WaitForSeconds(time);
            agente.speed = velocidade;
        }

        public void ComeçarAEscutar()
        {
            foreach (ObjetoDistracao objectdistraction in FindObjectsOfType<ObjetoDistracao>())
                objectdistraction.OnHitGround += ouvirObjeto;
        }

        public void PararDeEscutar()
        {
            foreach (ObjetoDistracao objectdistraction in FindObjectsOfType<ObjetoDistracao>())
                objectdistraction.OnHitGround -= ouvirObjeto;
        }

        protected virtual void ouvirObjeto(Vector3 objetoOuvido)
        {
            if (Vector3.Distance(objetoOuvido, transform.position) <= audioDetectionRadius) Mover(objetoOuvido);
        }
    }
}