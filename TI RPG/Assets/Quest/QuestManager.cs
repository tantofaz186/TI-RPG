using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    [SerializeField]private FetchQuest[] quests;
    [SerializeField]private HashSet<string> addedQuestNames = new HashSet<string>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        FetchQuest[] questObjects = FindObjectsOfType<FetchQuest>();
        for (int i = 0; i == questObjects.Length; i++)
        {
            if (!addedQuestNames.Contains(questObjects[i].gameObject.name))
            {
                quests[i] = questObjects[i];
                addedQuestNames.Add(questObjects[i].gameObject.name);
            }
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