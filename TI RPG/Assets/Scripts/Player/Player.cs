﻿using SaveGame;
using Skills;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerPassos))]
    [RequireComponent(typeof(PlayerDano))]
    public class Player : MonoBehaviour, ISaveable
    {
        
        public SaveData Save()
        {
            SaveData saveData = new SaveData
            {
                playerPositionX = transform.position.x,
                playerPositionY = transform.position.y,
                playerPositionZ = transform.position.z,
                xpPlayer = GetComponent<XpPlayer>()._xpAtual,
                armadihaFantasmaActive = GetComponent<ArmadilhaFantasma>().isActiveAndEnabled,
                invisibilidadeActive = GetComponent<Invisibilidade>().isActiveAndEnabled,
                lanternaEspectralActive = GetComponent<LanternaEspectral>().isActiveAndEnabled,
                mãosÁgeisActive = GetComponent<MãosÁgeis>().isActiveAndEnabled,
                mãosHábéisActive = GetComponent<MãosHábeis>().isActiveAndEnabled,
                passoFantasmaActive = GetComponent<PassoFantasma>().isActiveAndEnabled,
                premoniçãoActive = GetComponent<Premonição>().isActiveAndEnabled,
            };
            return saveData;
        }

        public void Load(SaveData saveData)
        {
            transform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY, saveData.playerPositionZ);
            GetComponent<XpPlayer>()._xpAtual = saveData.xpPlayer;
            GetComponent<ArmadilhaFantasma>().enabled = saveData.armadihaFantasmaActive;
            GetComponent<Invisibilidade>().enabled = saveData.invisibilidadeActive;
            GetComponent<LanternaEspectral>().enabled = saveData.lanternaEspectralActive;
            GetComponent<MãosÁgeis>().enabled = saveData.mãosÁgeisActive;
            GetComponent<MãosHábeis>().enabled = saveData.mãosHábéisActive;
            GetComponent<PassoFantasma>().enabled = saveData.passoFantasmaActive;
            GetComponent<Premonição>().enabled = saveData.premoniçãoActive;
            GetComponent<Proteção>().enabled = saveData.proteçãoActive;
        }
    }
}