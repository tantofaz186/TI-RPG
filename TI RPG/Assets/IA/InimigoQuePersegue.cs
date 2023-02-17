using System;
using System.Collections.Generic;
using UnityEngine;

namespace IA
{
    public class InimigoQuePersegue : Agente
    {
        [SerializeField] private List<Vector3> pontos;
        [SerializeField] private Transform alvo;
        public List<Vector3> Pontos => pontos;

        private void Awake()
        {
            SetState(new PatrulhaState(this, pontos));
        }

        protected override void Update()
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentState.GetType() == typeof(PatrulhaState)) SetState(new PerseguindoState(this, alvo));
                else SetState(new PatrulhaState(this, pontos));
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            foreach (var ponto in pontos)
            {
                Gizmos.DrawSphere(ponto, 0.5f);
            }

            Gizmos.color = Color.red;
            try
            {
                Gizmos.DrawSphere(alvo.position, 0.5f);
            }
            catch (Exception e)
            {
            }
        }
    }
}