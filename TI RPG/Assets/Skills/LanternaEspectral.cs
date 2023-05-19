using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEngine;

public class LanternaEspectral : Skill
{
    public override void Update()
    {
        
    }

    public override void OnEnable()
    {
        Debug.Log("Lanterna Espectral Ativada");
    }

    public override void OnDisable()
    {
        Debug.Log("Lanterna Espectral Desativada");
    }
}
