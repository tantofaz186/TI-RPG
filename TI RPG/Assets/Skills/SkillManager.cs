using Controllers;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Skills
{
    public class SkillManager : Singleton<SkillManager>
    {
        public static SkillManager instance;
        public Skill[] skills;
        public SkillUI[] skillButtons;
        public Skill activateSkill;
        public XpPlayer pontosPlayer;
    
        public void feedbackUpgrade()
        {
            for(int i=0; i < skills.Length; i++)
            {
                if (skills[i].isUpgrade)
                {
                    skills[i].GetComponent<Image>().color = new Vector4(1,1,1,1);
                }
                else
                {
                    skills[i].GetComponent<Image>().color = new Vector4(0.94f, 0.94f, 0.94f, 0.94f);
                }
            }
        }
        public void UpgradeButton()
        {
            if (!activateSkill.isUpgrade==false && pontosPlayer._xpAtual>1)
            {
                for (int i=1; i< activateSkill.skillAnterior.Length;i++)//coloquei como 1 para que uma das skills sempre esteja ativada para ter um ponto de partida
                {
                    if (activateSkill.skillAnterior[i].isUpgrade)
                    {
                        activateSkill.isUpgrade = true;
                        pontosPlayer._xpAtual -= 1;
                    }
                    else
                    {
                        Debug.Log("Você não pode desbloquear essa skill sem desbloquear as anteriores");
                    }
                }

            }
            else
            {
                Debug.Log("Essa skill não pode ser adiquirida");
            }
            feedbackUpgrade();
        }
    }
}
