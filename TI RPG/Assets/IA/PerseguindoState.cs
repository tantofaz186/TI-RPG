using UnityEngine;

namespace IA
{
    public class PerseguindoState : IState
    {
        public void OnEnter()
        {
            Debug.Log("Entrou no estado de perseguindo");
        }

        public void OnUpdate()
        {
            Debug.Log("Perseguindo");
        }

        public void OnExit()
        {
            Debug.Log("Saiu do estado de perseguindo");
        }
    }
}