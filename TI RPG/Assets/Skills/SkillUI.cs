using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillUI : MonoBehaviour
    {
        public Image skillImage => GetComponent<Image>();
        public Text skillNameText;
        public Text skillDesText;
        [SerializeField] Skill skill;
        public int skillButtonId;
    
        public void PressSkillButton()
        {
            SkillManager.instance.activateSkill = skill;
            skillImage.sprite = SkillManager.instance.skills[skillButtonId].skillSprite;
            skillNameText.text = SkillManager.instance.skills[skillButtonId].nomeSkill;
            skillDesText.text = SkillManager.instance.skills[skillButtonId].skillDescricao;
        }
    
    }
}
