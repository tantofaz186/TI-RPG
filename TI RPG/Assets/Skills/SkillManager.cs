using System;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillManager : MonoBehaviourSingletonPersistent<SkillManager>
    {
        public List<Skill> skills;
        public Skill activateSkill;
        public XpPlayer pontosPlayer;

        private void Start()
        {
            skills = GameObject.FindGameObjectWithTag("Player").GetComponents<Skill>().ToList();
        }

        public Skill SetActivateSkill(SkillUI skillUi)
        {
            if (skillUi.skill.Equals(null))
            {
                Debug.LogWarning("Skill não implementada");
                return null;
            }
            activateSkill = skillUi.skill;
            UpgradeButton(skillUi);
            return activateSkill;
        } 
        public void ResetSkills()
        {
            FindObjectsOfType<SkillUI>().ToList().ForEach(x => x.skillImage.color = Color.white);
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
        public void UpgradeButton(SkillUI skillUi)
        {
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
                skillUi.skillImage.color = Color.red;
                return;
            }
            Debug.Log("Essa skill não pode ser adiquirida");
        }
    }
}
