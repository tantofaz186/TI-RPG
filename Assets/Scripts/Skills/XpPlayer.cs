using UnityEngine;

namespace Player
{
    public class XpPlayer : MonoBehaviour
    {
        [SerializeField]
        public int _xpAtual;
    
        public void AddXp()
        {
            _xpAtual += 1;
        }
    }
}
