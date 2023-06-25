using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using Controllers;

public class QuestManager : MonoBehaviourSingletonPersistent<QuestManager>
{
    public static QuestManager instance;
    [SerializeField]private FetchQuest[] quests;
    [SerializeField]private HashSet<string> addedQuestNames = new HashSet<string>();

    public void ProcurarQuests()
    {

            FetchQuest[] questObjects = FindObjectsOfType<FetchQuest>();
            for (int i = 0; i == questObjects.Length; i++)
            {
                if (!addedQuestNames.Contains(questObjects[i].gameObject.name))
                {
                    quests[i] = questObjects[i];
                    addedQuestNames.Add(questObjects[i].gameObject.name);
                }
            }

        if (questObjects!=null)
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