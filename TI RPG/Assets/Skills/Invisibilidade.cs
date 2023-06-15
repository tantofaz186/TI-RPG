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
        bool ativada;
        public Text skillText;
        public override void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (!ativada)
                {
                    StartCoroutine(ActivatePremonicaoText(ativada));
                    StartCoroutine(FicarInvisivel());
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
        }
//        public override void Update()
//        { 
//            if (!Input.GetKeyDown(KeyCode.J)) return;
//            if (invisivel) return;
//            StopAllCoroutines();
//            StartCoroutine(FicarInvisivel());

//        }
        IEnumerator FicarInvisivel()
        {
            invisivel = true;
            m_MeshRenderer.enabled = !invisivel;
            m_Collider.enabled = !invisivel;
            yield return new WaitForSeconds(5f);
            invisivel = false;
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
