using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Controllers;
using Player;
using Skills;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveGame
{
    public class SaveGameManager : MonoBehaviourSingletonPersistent<SaveGameManager>
    {
        private string saveFilePath;
        public SaveData saveData;
        public override void Awake()
        {
            base.Awake();
            saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.dat");
        }

        public void SaveGame()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            saveData = new SaveData
            {
                playerPositionX = player.transform.position.x,
                playerPositionY = player.transform.position.y,
                playerPositionZ = player.transform.position.z,
                xpPlayer = player.GetComponent<XpPlayer>()._xpAtual,
                armadihaFantasmaActive = player.GetComponent<ArmadilhaFantasma>().isActiveAndEnabled,
                invisibilidadeActive = player.GetComponent<Invisibilidade>().isActiveAndEnabled,
                lanternaEspectralActive = player.GetComponent<LanternaEspectral>().isActiveAndEnabled,
                mãosÁgeisActive = player.GetComponent<MãosÁgeis>().isActiveAndEnabled,
                mãosHábéisActive = player.GetComponent<MãosHábeis>().isActiveAndEnabled,
                passoFantasmaActive = player.GetComponent<PassoFantasma>().isActiveAndEnabled,
                premoniçãoActive = player.GetComponent<Premonição>().isActiveAndEnabled,
                sceneName = UIControl.Instance.GetSceneName(),
            };
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
                saveData = (SaveData)formatter.Deserialize(fileStream);
                fileStream.Close();

                // Carregar os dados do save para o jogo
                UIControl.Instance.MudarCena(saveData.sceneName);
                SceneManager.sceneLoaded += LoadObjects;

                Debug.Log("Game loaded.");
            }
            else
            {
                Debug.Log("No save file found.");
            }
        }

        private void LoadObjects(Scene arg0, LoadSceneMode arg1)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position =
                new Vector3(saveData.playerPositionX, saveData.playerPositionY, saveData.playerPositionZ);
            player.GetComponent<XpPlayer>()._xpAtual = saveData.xpPlayer;
            player.GetComponent<ArmadilhaFantasma>().enabled = saveData.armadihaFantasmaActive;
            player.GetComponent<Invisibilidade>().enabled = saveData.invisibilidadeActive;
            player.GetComponent<LanternaEspectral>().enabled = saveData.lanternaEspectralActive;
            player.GetComponent<MãosÁgeis>().enabled = saveData.mãosÁgeisActive;
            player.GetComponent<MãosHábeis>().enabled = saveData.mãosHábéisActive;
            player.GetComponent<PassoFantasma>().enabled = saveData.passoFantasmaActive;
            player.GetComponent<Premonição>().enabled = saveData.premoniçãoActive;
            player.GetComponent<Proteção>().enabled = saveData.proteçãoActive;
            player.GetComponent<PlayerMovement>().Mover(player.transform.position);
            SceneManager.sceneLoaded -= LoadObjects;
        }
    }
}
