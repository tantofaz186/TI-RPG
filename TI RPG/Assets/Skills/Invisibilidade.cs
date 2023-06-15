using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class Invisibilidade : Skill
    {
        private GameObject player;
        private MeshRenderer m_MeshRenderer;
        private Collider m_Collider;
        private bool invisivel = false;
        public Text skillText;
        

        private IEnumerator ActivatePremonicaoText(bool active)
        {
            if (active)
            {
                skillText.text = "Invisibilidade Ativada";

                yield return new WaitForSeconds(3f);


                skillText.text = "";
            }
            else
            {
                skillText.text = "Invisibilidade Desativada";

                yield return new WaitForSeconds(3f);


                skillText.text = "";
            }
        }

        public override void OnEnable()
        {
            Debug.Log("Invisibilidade Ativada");
            player = GameObject.FindGameObjectWithTag("Player");
            m_MeshRenderer = player.GetComponent<MeshRenderer>();
            m_Collider = player.GetComponent<Collider>();
            skillText = FindObjectOfType<Text>();

        }

        public override void Update()
        {
            if (!Input.GetKeyDown(KeyCode.J)) return;
            if (invisivel) return;
            StopAllCoroutines();
            StartCoroutine(FicarInvisivel());

        }

        IEnumerator FicarInvisivel()
        {
            invisivel = true;
            StartCoroutine(ActivatePremonicaoText(invisivel));
            m_MeshRenderer.enabled = !invisivel;
            m_Collider.enabled = !invisivel;
            yield return new WaitForSeconds(5f);
            invisivel = false;
            StartCoroutine(ActivatePremonicaoText(invisivel));
            m_MeshRenderer.enabled = !invisivel;
            m_Collider.enabled = !invisivel;
        }

        public override void OnDisable()
        {
            Debug.Log("Invisibilidade Desativada");

            StopAllCoroutines();
            invisivel = false;
            m_MeshRenderer.enabled = !invisivel;
            m_Collider.enabled = !invisivel;
        }
    }
}