using System;
using UnityEngine;

namespace IA
{
    public class EncontrandoPlayerState : IState
    {
        float percentage = 0;
        float timeMultiplier = 1/5f;
        public event Action OnFoundPlayer;
        public event Action OnForgetPlayer;
        public void OnEnter()
        {
        }

        public void OnUpdate()
        {
            Debug.Log((percentage * 100) + "%");
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
        public void Encontrando()
        {
            percentage += Time.deltaTime * timeMultiplier * 2;
        }
        public void Perdendo()
        {
            percentage -= Time.deltaTime * timeMultiplier;
        }
        
        public void OnExit()
        {
        }
        public EncontrandoPlayerState(float percentage = 0)
        {
            this.percentage = percentage;
        }
    }

}