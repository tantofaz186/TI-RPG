using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class Premonição : Skill
    {
        [SerializeField] private List<Outline> outlines;
        [SerializeField] private GameObject[] inimigos;
        bool isActive = false;


        public override void OnEnable()
        {
            Debug.Log("Premonição Ativada");
            inimigos = GameObject.FindGameObjectsWithTag("Inimigo");
            foreach (var inimigo in inimigos)
            {
                Outline _outline = inimigo.AddComponent<Outline>();
                PrepareOutline(_outline);
            }
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
