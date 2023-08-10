using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Skills
{
    public class Premonição : Skill
    {
        [SerializeField] private List<Outline> outlines;
        [SerializeField] private GameObject[] inimigos;
        bool isActive = false;
        public Text skillText;
        private IEnumerator ActivatePremonicaoText(bool active)
        {
            if (active) 
            {
                skillText.text = "Premonição Ativada";

                yield return new WaitForSeconds(3f);


                skillText.text = "";
            }
            else
            {
                skillText.text = "Premonição Desativada";

                yield return new WaitForSeconds(3f);


                skillText.text = "";
            }
        }
        public override void OnEnable()
        {
            inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            foreach (var inimigo in inimigos)
            {
                Outline _outline = inimigo.AddComponent<Outline>();
                PrepareOutline(_outline);
            }
            GameObject skillTextObject = GameObject.Find("cheatSkillText");
            skillText = skillTextObject.GetComponent<Text>();
        }

        private void PrepareOutline(Outline _outline)
        {
            _outline.OutlineColor = Color.red;
            _outline.OutlineMode = Outline.Mode.OutlineAll;
            _outline.OutlineWidth = 2f;
            _outline.enabled = false;
            outlines.Add(_outline);
        }   
          public override void Update()
          {
              if (!Input.GetKeyDown(KeyCode.G)) return;
              isActive = !isActive;
              StartCoroutine(ActivatePremonicaoText(isActive));
              foreach (var outline in outlines)
              {
                  outline.enabled = isActive;
              }
          }

        public override void OnDisable()
        {
            isActive = false;
            foreach (var outline in outlines)
            {
                outline.enabled = isActive;
            }
            Debug.Log("Premonição Desativada");
        }

       
    }
}

