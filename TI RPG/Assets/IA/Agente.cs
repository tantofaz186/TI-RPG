using Unity.VisualScripting;
using UnityEngine;

namespace IA
{
    public class Agente : StateMachine
    {
        [SerializeField] protected float velocidade = 5f;
        
        public void Mover(Vector3 ponto)
        {
            transform.position = Vector3.MoveTowards(transform.position, ponto, velocidade * Time.deltaTime);
        }
    }
}