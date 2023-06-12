using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

public class TutorialManager : Singleton<TutorialManager>
{
    public Dialogue firstDialogue;

    private void Start()
    {
        DialogueManager.Instance.StartDialogue(firstDialogue);
    }
}
