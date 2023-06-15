using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Skills;

using UnityEngine.UI;

namespace Skills
{
    public class PassoFantasma : Skill
    {
        bool ativada;
        public Text skillText;
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
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
                skillText.text = "Passo Fantasma Ativada";

                yield return new WaitForSeconds(3f);


                skillText.text = "";
            }
            else
            {
                skillText.text = "Passo Fantasma Desativada";

                yield return new WaitForSeconds(3f);


                skillText.text = "";
            }
        }
        public override void OnEnable()
        {
            Debug.Log("Passo Fantasma Ativado");
        }
        public override void OnDisable()
        {
            Debug.Log("Passo Fantasma Desativado");
        }
    }
}
