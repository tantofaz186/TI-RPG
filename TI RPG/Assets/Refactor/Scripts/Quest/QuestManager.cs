using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Rpg.Crafting;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Refactor.Scripts.Quest
{
    public class QuestManager : Singleton<QuestManager>
    {
        public List<Quest> quests;

        private void OnEnable()
        {
            quests.ForEach(q => q.IsCompleted = false);
            StartCoroutine(RegisterFirstQuest());
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            foreach (Quest quest in quests)
            {
                quest._OnDisable();
                quest.OnComplete -= OnQuestComplete;
            }
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.buildIndex == 2)
            {
                StartCoroutine(RegisterQuests());
            }
        }

        private IEnumerator RegisterFirstQuest()
        {
            yield return new WaitUntil(() => DialogueManager.Instance != null);
            yield return new WaitUntil(() => PlayerInventory.Instance != null);
            yield return new WaitForEndOfFrame();
            foreach (Quest quest in quests.Where(q => q.objectives[0].GetType() == typeof(LoadSceneObjective)))
            {
                quest._OnEnable();
                quest.OnComplete += OnQuestComplete;
            }
        }

        private IEnumerator RegisterQuests()
        {
            yield return new WaitUntil(() => DialogueManager.Instance != null);
            yield return new WaitUntil(() => PlayerInventory.Instance != null);
            yield return new WaitForEndOfFrame();
            foreach (Quest quest in quests.Where(q => !q.IsCompleted))
            {
                quest._OnEnable();
                quest.OnComplete += OnQuestComplete;
            }
        }

        private void OnQuestComplete(List<Rewards> rewardsList)
        {
            foreach (Rewards reward in rewardsList)
            {
                if (reward.removeOnComplete)
                {
                    PlayerInventory.Instance.RemoveItem(reward.item);
                }
                else if (!PlayerInventory.Instance.AddItem(reward.item))
                {
                    PlayerInventory.Instance.DropItem(reward.item);
                }
            }
        }

        #if UNITY_EDITOR
        private void OnValidate()
        {
            quests = GetAllInstances<Quest>().ToList();
        }

        private IEnumerable<T> GetAllInstances<T>() where T : ScriptableObject
        {
            return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);
        }
        #endif
    }

    #if UNITY_EDITOR

    [CustomEditor(typeof(QuestManager))]
    public class QuestManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Auto Register"))
            {
                QuestManager questManager = (target as QuestManager)!;
                questManager.quests = GetAllInstances<Quest>().ToList();
            }
        }

        private IEnumerable<T> GetAllInstances<T>() where T : ScriptableObject
        {
            return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);
        }
    }

    #endif
}