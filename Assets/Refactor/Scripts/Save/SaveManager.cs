using System;
using System.IO;
using Controllers;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rpg.Save
{
    public class SaveManager : Singleton<SaveManager>
    {
        public static string FilePath { get; private set; } = "?";
        public static Action mustReloadData;
        
        [Serializable]
        public class SaveData
        {
            public string[] playerInventory;
        }

        public SaveData data;

        private void OnEnable()
        {
            FilePath = Application.persistentDataPath + "/save.dat";
        }

        public void Save()
        {
            string content = JsonUtility.ToJson(data);
            File.WriteAllText(FilePath, content);
        }

        public void Load()
        {
            string content = File.ReadAllText(FilePath);
            data = JsonUtility.FromJson<SaveData>(content);
            
            mustReloadData?.Invoke();
        }

        public bool HasSave()
        {
            return File.Exists(FilePath);
        }

        public void Delete()
        {
            File.Delete(FilePath);
        }
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(SaveManager))]
    public class SaveManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SaveManager manager = (target as SaveManager)!;

            if (Application.isPlaying)
            {
                EditorGUI.BeginDisabledGroup(true);
                EditorGUILayout.TextField("Path", SaveManager.FilePath);
                EditorGUI.EndDisabledGroup();
            }

            if (GUILayout.Button("Save"))
            {
                manager.Save();
            }
            
            if (GUILayout.Button("Load"))
            {
                manager.Load();
            }
            
            if (GUILayout.Button("Delete"))
            {
                manager.Load();
            }
        }
    }
    #endif
}
