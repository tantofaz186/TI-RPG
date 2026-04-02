using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

using UnityEngine.UI;


public class MãosHábeis : Skill
{
    bool ativada;
    public Text skillText;
    public override void Update()
    {
       
    }
    public void Start()
    {
        ativada = true;
        skillText = GameObject.FindObjectOfType<Text>();
    }
    private IEnumerator ActivatePremonicaoText(bool active)
    {
        if (active)
        {
            skillText.text = "Mãos Hábeis Ativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
        else
        {
            skillText.text = "Mãos Hábeis Desativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
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
