using Skills;
using UnityEngine;

public class ArmadilhaFantasma : Skill
{
    public GameObject armadilha;
    public GameObject player;


    public override void OnEnable()
    {
        Debug.Log("Armadilha Fantasma Ativada");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(armadilha, player.transform.position, Quaternion.identity);
        }
    }
    
    public override void OnDisable()
    {
        Debug.Log("Armadilha Fantasma Desativada");
    }
}
