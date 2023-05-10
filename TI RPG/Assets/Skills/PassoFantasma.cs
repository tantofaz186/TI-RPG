using System;
using UnityEngine;

namespace Skills
{
    public class PassoFantasma : Skill
    {
        public override void OnEnable()
        {
            Debug.Log("Passo Fantasma Ativado");
        }
        public override void Update()
        {
            
        }
        public override void OnDisable()
        {
            Debug.Log("Passo Fantasma Desativado");
        }
    }
}
