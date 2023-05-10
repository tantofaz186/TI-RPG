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

}
