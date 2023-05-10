using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillUI : MonoBehaviour
    {
        public Image skillImage => GetComponent<Image>();
        public Text skillNameText;
        public Text skillDesText;

        public int skillButtonId;
    
        public void PressSkillButton()
        {
            SkillManager.instance.activateSkill = GetComponent<Skill>();
            skillImage.sprite = SkillManager.instance.skills[skillButtonId].skillSprite;
            skillNameText.text = SkillManager.instance.skills[skillButtonId].nomeSkill;
            skillDesText.text = SkillManager.instance.skills[skillButtonId].skillDescricao;
        }
    
    }
}
