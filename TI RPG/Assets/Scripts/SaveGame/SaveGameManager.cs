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
                
                xpPlayer = SkillManager.Instance.GetComponent<XpPlayer>()._xpAtual,
                armadihaFantasmaActive = SkillManager.Instance.GetComponent<ArmadilhaFantasma>().isActiveAndEnabled,
                invisibilidadeActive = SkillManager.Instance.GetComponent<Invisibilidade>().isActiveAndEnabled,
                lanternaEspectralActive = SkillManager.Instance.GetComponent<LanternaEspectral>().isActiveAndEnabled,
                mãosÁgeisActive = SkillManager.Instance.GetComponent<MãosÁgeis>().isActiveAndEnabled,
                mãosHábéisActive = SkillManager.Instance.GetComponent<MãosHábeis>().isActiveAndEnabled,
                passoFantasmaActive = SkillManager.Instance.GetComponent<PassoFantasma>().isActiveAndEnabled,
                premoniçãoActive = SkillManager.Instance.GetComponent<Premonição>().isActiveAndEnabled,
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
            SkillManager.Instance.GetComponent<XpPlayer>()._xpAtual = saveData.xpPlayer;
            SkillManager.Instance.GetComponent<ArmadilhaFantasma>().enabled = saveData.armadihaFantasmaActive;
            SkillManager.Instance.GetComponent<Invisibilidade>().enabled = saveData.invisibilidadeActive;
            SkillManager.Instance.GetComponent<LanternaEspectral>().enabled = saveData.lanternaEspectralActive;
            SkillManager.Instance.GetComponent<MãosÁgeis>().enabled = saveData.mãosÁgeisActive;
            SkillManager.Instance.GetComponent<MãosHábeis>().enabled = saveData.mãosHábéisActive;
            SkillManager.Instance.GetComponent<PassoFantasma>().enabled = saveData.passoFantasmaActive;
            SkillManager.Instance.GetComponent<Premonição>().enabled = saveData.premoniçãoActive;
            SkillManager.Instance.GetComponent<Proteção>().enabled = saveData.proteçãoActive;
            player.GetComponent<PlayerMovement>().Mover(player.transform.position);
            SceneManager.sceneLoaded -= LoadObjects;
        }
    }
}
