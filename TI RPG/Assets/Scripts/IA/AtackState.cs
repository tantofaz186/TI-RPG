using UnityEngine;

namespace IA
{
    public class AtackState : IState
    {
        private static readonly int Atacando = Animator.StringToHash("atacando");
        private static readonly int Movimentando = Animator.StringToHash("movimentando");
        private readonly Agente self;
        private readonly Transform target;

        public AtackState(Agente self, Transform target)
        {
            this.target = target;
            this.self = self;
        }

        public void OnEnter()
        {
        }

        public void OnUpdate()
        {
            if (Vector3.Distance(self.transform.position, target.position) < 2.5f)
            {
                self.Mover(self.transform.position);
                self.Animator.SetTrigger(Atacando);
                self.Animator.SetBool(Movimentando, false);
            }
            else
            {
                self.Mover(target.position);
            }
        }

        public void OnExit()
        {
        }
    }
}