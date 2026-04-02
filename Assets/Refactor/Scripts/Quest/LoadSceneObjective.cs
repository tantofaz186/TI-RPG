using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Refactor.Scripts.Quest
{
    [CreateAssetMenu(fileName = "New Quest Objective", menuName = "RPG/Quests/Objectives/Load Scene Objective")]
    public class LoadSceneObjective : QuestObjective
    {
        public int sceneBuildIndex;

        public override void _OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public override void _OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex == sceneBuildIndex)
            {
                CompleteObjective();
            }
        }
    }
    #if UNITY_EDITOR
    [CustomEditor(typeof(LoadSceneObjective))]
    public class LoadSceneObjectiveEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            LoadSceneObjective objective = (LoadSceneObjective)target;
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(objective.sceneBuildIndex);
            int sceneIndex = SceneUtility.GetBuildIndexByScenePath(pathToScene);
            string sceneName = Path.GetFileNameWithoutExtension(pathToScene);
            bool isValidScene = File.Exists(pathToScene);

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Scene Info", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.enabled = false;
            EditorGUILayout.TextField("Scene ", sceneName);
            EditorGUILayout.IntField("Index ", sceneIndex);
            EditorGUILayout.Toggle("Valid Scene", isValidScene);

            GUI.enabled = true;
            EditorGUILayout.EndVertical();
            if (!isValidScene) EditorGUILayout.HelpBox("Scene is not valid", MessageType.Error);
        }
    }
    #endif
}