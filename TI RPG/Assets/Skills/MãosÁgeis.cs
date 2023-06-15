using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

using UnityEngine.UI;

public class MãosÁgeis : Skill
{
    bool ativada;
    public Text skillText;
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
            skillText.text = "Mãos Ágeis Ativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
        else
        {
            skillText.text = "Mãos Ágeis Desativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
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
