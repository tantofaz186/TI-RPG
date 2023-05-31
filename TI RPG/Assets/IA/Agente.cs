using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace IA
{
    public class Agente : StateMachine
    {
        [SerializeField] protected float velocidade = 5f;
        [SerializeField] protected NavMeshAgent agente;
        [SerializeField] protected float audioDetectionRadius = 10f;

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

        public void ComeçarAEscutar(){
            foreach (ObjetoDistracao objectdistraction in FindObjectsOfType<ObjetoDistracao>())
            {
                objectdistraction.onhitground += ouvirObjeto;
            }
        }
        
        public void PararDeEscutar(){
            foreach (ObjetoDistracao objectdistraction in FindObjectsOfType<ObjetoDistracao>())
            {
                objectdistraction.onhitground -= ouvirObjeto;
            }
        }
        protected virtual void ouvirObjeto(Vector3 objetoOuvido)
        {
            if (Vector3.Distance(objetoOuvido, transform.position) <= audioDetectionRadius)
            {
                Mover(objetoOuvido);
            }
        }
    }
}