using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public string nomeSkill;
    public Sprite skillSprite;

    [TextArea(1, 3)]
    public string skillDescricao;
    public bool isUpgrade;
    public Skill[] skillAnterior;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
