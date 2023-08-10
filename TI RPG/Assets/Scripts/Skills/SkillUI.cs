using System;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillUI : MonoBehaviour
    {
        
        public Image skillImage => GetComponent<Image>();
        //public Text skillNameText;
        //public Text skillDesText;
        public Skill skill;

        public void PressSkillButton()
        {
            SkillManager.Instance.SetActivateSkill(this);
        }
    
    }
}
