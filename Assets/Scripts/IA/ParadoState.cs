using UnityEngine;

namespace IA
{
    public class ParadoState : IState
    {
        private readonly Vector3 position;
        private readonly Quaternion rotation;
        private readonly Agente self;

        private float totalTime;

        public ParadoState(Agente self, Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
            this.self = self;
            totalTime = 0;
        }

        public void OnEnter()
        {
            self.Mover(position);
        }

        public void OnUpdate()
        {
            if (Vector3.Distance(self.transform.position, position) > 0.2f)
                return;
            if (totalTime >= 1)
                return;
            totalTime += Time.deltaTime / 10f;
            self.transform.eulerAngles =
                Vector3.Lerp(self.transform.rotation.eulerAngles, rotation.eulerAngles, totalTime);
        }

        public void OnExit()
        {
            totalTime = 0;
        }
    }
}