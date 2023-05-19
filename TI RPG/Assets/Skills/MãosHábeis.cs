using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;


public class MãosHábeis : Skill
{
    public override void Update()
    {
        
    }

    public override void OnEnable()
    {
        Debug.Log("Mãos Hábeis Ativada");
    }

    public override void OnDisable()
    {
        Debug.Log("Mãos Hábeis Desativada");
    }
}
