using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEngine;
using UnityEngine.UI;

public class LanternaEspectral : Skill
{
    bool isActive = false;
    bool ativada;
    public Text skillText;
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!ativada)
            {
                StartCoroutine(ActivatePremonicaoText(ativada));
                ativada = true;
            }
            else
            {
                StartCoroutine(ActivatePremonicaoText(ativada));
                ativada = false;
            }
        }
    }
    public void Start()
    {
        ativada = true;
        GameObject skillTextObject = GameObject.Find("cheatSkillText");
        skillText = skillTextObject.GetComponent<Text>();
    }

    public override void OnEnable()
    {
        Debug.Log("Lanterna Espectral Ativada");
    }

    public override void OnDisable()
    {
        Debug.Log("Lanterna Espectral Desativada");
    }

    private IEnumerator ActivatePremonicaoText(bool active)
    {
        if (active)
        {
            skillText.text = "Lanterna Espectral Ativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
        else
        {
            skillText.text = "Lanterna Espectral Desativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
    }
}
