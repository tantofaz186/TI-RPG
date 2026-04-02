using UnityEngine;

namespace IA
{
    public class MovendoParaPosicaoInicialState : IState
    {
        private readonly Agente self;
        private readonly Transform target;

        public MovendoParaPosicaoInicialState(Agente self, Transform target)
        {
            this.target = target;
            this.self = self;
        }

        public void OnEnter()
        {
        }

        public void OnUpdate()
        {
            self.Mover(target.position);
        }

        public void OnExit()
        {
        }
    }
}