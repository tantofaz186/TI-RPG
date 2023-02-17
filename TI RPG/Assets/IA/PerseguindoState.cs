using UnityEngine;

namespace IA
{
    public class PerseguindoState : IState
    {
        private Agente self;
        private Transform target;
        public void OnEnter()
        {
            Debug.Log("Entrou no estado de perseguindo");
        }

        public void OnUpdate()
        {
            Debug.Log("Perseguindo");
            self.Mover(target.position);
            if (Vector3.Distance(self.transform.position, target.position) < 0.5f)
            {
                InimigoQuePersegue _self = self as InimigoQuePersegue;
                self.SetState(new PatrulhaState(self, _self.Pontos));
            }
        }

        public void OnExit()
        {
            Debug.Log("Saiu do estado de perseguindo");
        }
        public PerseguindoState(Agente self, Transform target)
        {
            this.target = target;
            this.self = self;
        }
    }
}