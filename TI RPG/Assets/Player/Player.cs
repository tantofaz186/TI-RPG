using SaveGame;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerPassos))]
    [RequireComponent(typeof(XpPlayer))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerMovement))]
    public class Player : MonoBehaviour, ISaveable
    {
        
        public SaveData Save()
        {
            throw new System.NotImplementedException();
        }

        public void Load(SaveData saveData)
        {
            throw new System.NotImplementedException();
        }
    }
}