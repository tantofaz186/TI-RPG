using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quests
{
    public class QuestManager : MonoBehaviourSingletonPersistent<QuestManager>
    {
        [SerializeField]private List<FetchQuest.FetchQuest> quests;
        [SerializeField]private HashSet<string> addedQuestNames = new HashSet<string>();

        private void Start()
        {
            SceneManager.sceneLoaded += ProcurarQuests;
        }

        public void ProcurarQuests(Scene arg0, LoadSceneMode loadSceneMode)
        {
            if (arg0 != SceneManager.GetSceneByBuildIndex(2)) return;
            foreach (var quest in FindObjectsOfType<FetchQuest.FetchQuest>())
            {
                if (!addedQuestNames.Contains(quest.gameObject.name))
                {
                    quests.Add(quest);
                    addedQuestNames.Add(quest.gameObject.name);
                }
            }

            if (quests.Count > 0)
            {
                Debug.Log("Encontrei quests!");
            }
        }

        public void AtualizarQuests()
        {
            FetchQuest.FetchQuest[] questObjects = FindObjectsOfType<FetchQuest.FetchQuest>();
            for (int i=0; i== questObjects.Length; i++)
            {
                if (questObjects[i].questConcluida==true && quests[i].questConcluida == false)
                {
                    quests[i].questConcluida = true;
                }
            }
       
        }
    }
}