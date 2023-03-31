using UnityEngine;
using UnityEngine.AI;

namespace IA
{
    public class Agente : StateMachine
    {
        [SerializeField] protected float velocidade = 5f;
        [SerializeField] protected NavMeshAgent agente;

        public void Mover(Vector3 ponto)
        {
            if (agente.hasPath)
                agente.Move(Vector3.MoveTowards(transform.position, ponto, velocidade * Time.deltaTime));
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, ponto, velocidade * Time.deltaTime);    
            }
            Vector3 relativePos = ponto - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp( transform.rotation, toRotation, 3 * Time.deltaTime );
        }
    }
}