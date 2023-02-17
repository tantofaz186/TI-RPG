using System;
using UnityEngine;

namespace IA
{
    public class InimigoQuePersegue : Agente
    {
        private void Awake()
        {
            SetState(new PatrulhaState());
        }
        protected override void Update() 
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentState.GetType() == typeof(PatrulhaState)) SetState(new PatrulhaState());
                else SetState(new PatrulhaState());
            }
        }
    }
}