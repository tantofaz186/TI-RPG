using System.Collections.Generic;
using Controllers;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillManager : Singleton<SkillManager>
    {
        public List<Skill> skills;
        public Skill activateSkill;
        public XpPlayer pontosPlayer;
        
        
        public Skill SetActivateSkill(SkillUI skillUi)
        {
            //int index = skills.FindIndex(x => skillUi.skill.name == x.name);
            //skillUi.skillImage.sprite = skills[index].skillSprite;
            //skillUi.skillNameText.text = skills[index].nomeSkill;
            //skillUi.skillDesText.text = skills[index].skillDescricao;
            activateSkill = skillUi.skill;
            UpgradeButton();
            return activateSkill;
        } 
        public void ResetSkills()
        {
            int refundSkillPoints = 0;
            foreach (var skill in skills)
            {
                if (skill.enabled)
                {                
                    skill.enabled = false;
                    refundSkillPoints++;
                }
            }
            pontosPlayer._xpAtual += refundSkillPoints;
        }
        /*public void feedbackUpgrade()
        {
            for(int i=0; i < skills.Count; i++)
            {
                if (skills[i].enabled)
                {
                    skills[i].GetComponent<Image>().color = new Vector4(1,1,1,1);
                }
                else
                {
                    skills[i].GetComponent<Image>().color = new Vector4(0.94f, 0.94f, 0.94f, 0.94f);
                }
            }
        }*/
        public void UpgradeButton()
        {
            Debug.Log(!activateSkill.enabled);
            Debug.Log(pontosPlayer._xpAtual>=1);
            if (!activateSkill.enabled && pontosPlayer._xpAtual>=1)
            {
                bool canUnlock = true;
                foreach (var t in activateSkill.skillAnterior)
                {
                    if (t.enabled) continue;
                    Debug.Log("Você não pode desbloquear essa skill sem desbloquear as anteriores");
                    canUnlock = false;

                }
                if (!canUnlock) return;
                pontosPlayer._xpAtual -= 1;
                activateSkill.enabled = true;
                return;
            }
            Debug.Log("Essa skill não pode ser adiquirida");
        }
    }
}
