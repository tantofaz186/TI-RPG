using UnityEngine;

namespace IA
{
    public class StateMachine : MonoBehaviour
    {
        protected IState currentState;
        
        public void SetState(IState state)
        {
            currentState?.OnExit();

            currentState = state;

            currentState.OnEnter();
        }
        protected virtual void Update()
        {
            currentState?.OnUpdate();
        }
    }
}