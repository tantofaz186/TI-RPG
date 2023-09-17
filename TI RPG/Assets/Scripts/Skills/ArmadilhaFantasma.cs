using System.Collections;
using Skills;
using UnityEngine;
using UnityEngine.UI;

public class ArmadilhaFantasma : Skill
{
    public GameObject armadilha;
    public GameObject player;

    bool ativada;
    public Text skillText;
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!ativada)
            {
                StartCoroutine(ActivatePremonicaoText(ativada));
                ativada = true;
            }
            else
            {
                StartCoroutine(ActivatePremonicaoText(ativada));
                ativada = false;
            }
        }
    }
    public void Start()
    {
        ativada = true;
        GameObject skillTextObject = GameObject.Find("cheatSkillText");
        skillText = skillTextObject.GetComponent<Text>();
    }
    private IEnumerator ActivatePremonicaoText(bool active)
    {
        if (active)
        {
            skillText.text = "Armadilha Fantasma Ativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
        else
        {
            skillText.text = "Armadilha Fantasma Desativada";

            yield return new WaitForSeconds(3f);


            skillText.text = "";
        }
    }
    public override void OnEnable()
    {
        Debug.Log("Armadilha Fantasma Ativada");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
//   public override void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            Instantiate(armadilha, player.transform.position, Quaternion.identity);
//        }
//    }

    
    public override void OnDisable()
    {
        Debug.Log("Armadilha Fantasma Desativada");
    }
}
