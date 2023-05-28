
using UnityEngine;

namespace SaveGame
{
    [System.Serializable]
    public class SaveData
    {
        public Vector3 playerPosition;
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
    }
}