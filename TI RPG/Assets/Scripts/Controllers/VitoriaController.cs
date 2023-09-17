using UnityEngine;

namespace Controllers
{
    public class VitoriaController : MonoBehaviourSingletonPersistent<VitoriaController>
    {
        [SerializeField] GameObject vitoriaScreen;

        public void Ganhou()
        {
            vitoriaScreen.SetActive(true);
        }
        
    }
}