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
                quest.OnComplete += OnQuestComplete;
                quest.OnEnable();
            }
        }

        private void OnDisable()
        {
            foreach (Quest quest in quests) quest.OnComplete -= OnQuestComplete;
        }

        private void OnQuestComplete(Item reward)
        {
            if (!PlayerInventory.Instance.AddItem(reward)) PlayerInventory.Instance.DropItem(reward);
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