using UnityEngine;

namespace IA
{
    public class PerseguindoState : IState
    {
        private Agente self;
        private Transform target;
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
        public PerseguindoState(Agente self, Transform target)
        {
            this.target = target;
            this.self = self;
        }
    }
}