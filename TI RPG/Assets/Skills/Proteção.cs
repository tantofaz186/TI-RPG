using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

public class Proteção : Skill
{
    public override void Update()
    {
        
    }

    public override void OnEnable()
    {
        Debug.Log("Proteção Ativada");
    }

    public override void OnDisable()
    {
        Debug.Log("Proteção Desativada");
    }
}
