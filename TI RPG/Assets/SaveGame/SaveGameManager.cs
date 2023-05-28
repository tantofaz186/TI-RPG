using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Controllers;
using UnityEngine;

namespace SaveGame
{
    public class SaveGameManager : Singleton<SaveGameManager>
    {
        private string saveFilePath;
        public SaveData saveData;
        private void Awake()
        {
            saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.dat");
        }

        public void SaveGame(SaveData _saveData)
        {
            SaveData saveData = _saveData;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = File.Create(saveFilePath);
            formatter.Serialize(fileStream, saveData);
            fileStream.Close();

            Debug.Log("Game saved.");
        }

        public void LoadGame()
        {
            if (File.Exists(saveFilePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream fileStream = File.Open(saveFilePath, FileMode.Open);
                SaveData saveData = (SaveData)formatter.Deserialize(fileStream);
                fileStream.Close();

                // Carregar os dados do save para o jogo
                List<ISaveable> GameObjectsToLoad = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();

                foreach (var saveable in GameObjectsToLoad)
                {
                    saveable.Load(saveData);
                }

                Debug.Log("Game loaded.");
            }
            else
            {
                Debug.Log("No save file found.");
            }
        }
    }
}
