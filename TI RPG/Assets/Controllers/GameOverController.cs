using UnityEngine;

namespace Controllers
{
    public class GameOverController : Singleton<GameOverController>
    {
        [SerializeField] GameObject gameOverScreen;

        public void GameOver()
        {
            gameOverScreen.SetActive(true);
        }
        
    }
}