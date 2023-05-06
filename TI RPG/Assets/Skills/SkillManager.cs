using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Skill[] skills;
    public SkillUI[] skillButtons;
    public Skill activateSkill;
    public XpPlayer pontosPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
