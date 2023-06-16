using UnityEngine;

namespace Controllers
{
    public class GameOverController : MonoBehaviourSingletonPersistent<GameOverController>
    {
        [SerializeField] GameObject gameOverScreen;

        public void GameOver()
        {
            gameOverScreen.SetActive(true);
        }
        
    }
}