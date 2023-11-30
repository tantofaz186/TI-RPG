using System;
using UnityEngine;

namespace IA
{
    public class EncontrandoPlayerState : IState
    {
        private readonly float timeMultiplier = 1 / 5f;
        private float percentage;

        public EncontrandoPlayerState(float percentage = 0, float timeMultiplier = 1 / 5f)
        {
            this.percentage = percentage;
            this.timeMultiplier = timeMultiplier;
        }

        public void OnEnter()
        {
        }

        public void OnUpdate()
        {
            Debug.Log(percentage * 100 + "%");
            switch (percentage)
            {
                case >= 1:
                    OnFoundPlayer?.Invoke();
                    break;
                case <= 0:
                    OnForgetPlayer?.Invoke();
                    break;
            }

            Perdendo();
        }

        public void OnExit()
        {
        }

        public event Action OnFoundPlayer;
        public event Action OnForgetPlayer;

        public void Encontrando()
        {
            percentage += Time.deltaTime * timeMultiplier;
        }

        public void Perdendo()
        {
            percentage -= Time.deltaTime * timeMultiplier / 2;
        }
    }
}