using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

using UnityEngine.UI;


public class Proteção : Skill
{
    bool ativada;
    public Text skillText;
    public override void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Q)) return;
        ativada = !ativada;
        StopAllCoroutines();
        StartCoroutine(ActivatePremonicaoText(ativada));
    }
    private IEnumerator ActivatePremonicaoText(bool active)
    {
        if (active)
        {
            skillText.text = "Proteção Ativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
        else
        {
            skillText.text = "Proteção Desativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
    }
    public override void OnEnable()
    {
        Debug.Log("Proteção Ativada");
        ativada = true;
        GameObject skillTextObject = GameObject.Find("cheatSkillText");
        skillText = skillTextObject.GetComponent<Text>();
    }

    public override void OnDisable()
    {
        Debug.Log("Proteção Desativada");
    }
}
