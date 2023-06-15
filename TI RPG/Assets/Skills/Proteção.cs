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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!ativada)
            {
                StartCoroutine(ActivatePremonicaoText(ativada));
                ativada = true;
            }
            else
            {
                StartCoroutine(ActivatePremonicaoText(ativada));
                StopAllCoroutines();
                ativada = false;
            }
        }
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
    }

    public override void OnDisable()
    {
        Debug.Log("Proteção Desativada");
    }
}
