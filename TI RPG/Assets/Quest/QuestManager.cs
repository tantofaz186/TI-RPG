using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Controllers;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviourSingletonPersistent<QuestManager>
{
    [SerializeField]private FetchQuest[] quests;
    [SerializeField]private HashSet<string> addedQuestNames = new HashSet<string>();

    private void Start()
    {
        SceneManager.sceneLoaded += ProcurarQuests;
    }

    public void ProcurarQuests(Scene arg0, LoadSceneMode loadSceneMode)
    {
        if (arg0 != SceneManager.GetSceneByBuildIndex(2)) return;
        FetchQuest[] questObjects = FindObjectsOfType<FetchQuest>();
        for (int i = 0; i == questObjects.Length; i++)
        {
            if (!addedQuestNames.Contains(questObjects[i].gameObject.name))
            {
                quests[i] = questObjects[i];
                addedQuestNames.Add(questObjects[i].gameObject.name);
            }
        }

        if (questObjects.Length > 0)
        {
            Debug.Log("Encontrei quests!");
        }
    }

    public void AtualizarQuests()
    {
        FetchQuest[] questObjects = FindObjectsOfType<FetchQuest>();
        for (int i=0; i== questObjects.Length; i++)
        {
            if (questObjects[i].questConcluida==true && quests[i].questConcluida == false)
            {
                quests[i].questConcluida = true;
            }
        }
       
    }
}