using System.Collections.Generic;
using System.Linq;
using Controllers;
using Rpg.Crafting;
using UnityEngine;
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
            foreach (Quest quest in quests)
            {
                quest._OnEnable();
                quest.OnComplete += OnQuestComplete;
            }
        }

        private void OnDisable()
        {
            foreach (Quest quest in quests)
            {
                quest._OnDisable();
                quest.OnComplete -= OnQuestComplete;
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