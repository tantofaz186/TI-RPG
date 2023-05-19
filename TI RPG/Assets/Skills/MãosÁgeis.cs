using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

public class MãosÁgeis : Skill
{

    public override void Update()
    {
        
    }

    public override void OnEnable()
    {
        Debug.Log("Mãos Ágeis Ativada");
    }

    public override void OnDisable()
    {
        Debug.Log("Mãos Ágeis Desativada");
    }
}
