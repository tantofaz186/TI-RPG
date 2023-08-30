
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaveGame
{
    [System.Serializable]
    public class SaveData
    {
        public float playerPositionX, playerPositionY, playerPositionZ;
        public int xpPlayer;
        public bool armadihaFantasmaActive;
        public bool invisibilidadeActive;
        public bool lanternaEspectralActive;
        public bool mãosÁgeisActive;
        public bool mãosHábéisActive;
        public bool passoFantasmaActive;
        public bool premoniçãoActive;
        public bool proteçãoActive;
        public bool isEscondido;
        public bool isHoldingItem;
        public string sceneName;
    }
}