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
    }
}