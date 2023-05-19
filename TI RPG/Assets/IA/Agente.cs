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
            //StartCoroutine(RotateToNextPosition());
        }
        /*private IEnumerator RotateToNextPosition()
        {
            agente.isStopped = true;
            while (Mathf.Abs(rotateAngle) > 0.1f)
            {
                transform.Rotate(Vector3.up, rotateAngle * 20 * Time.deltaTime);
                yield return null;
            }
            agente.isStopped = false;
        }

        private float rotateAngle
        {
            get
            {
                Vector3 direction = agente.transform.forward - transform.position;
                return Vector3.SignedAngle(transform.forward, direction, transform.up) / 180f;
            }
        }*/
        public IEnumerator GetSpeedBoost(float time, float multiplier)
        {
            agente.speed = velocidade * multiplier;
            yield return new WaitForSeconds(time);
            agente.speed = velocidade;
        }
    }
}