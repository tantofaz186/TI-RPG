using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseSystem : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public 
    void Start()
    {
        pauseMenu.SetActive(false);
        gameIsPaused = false;
    }
    void Update()
    {
     /*  if (SceneManager.sceneCountInBuildSettings != 0) 
       {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = !gameIsPaused;
                pauseMenu.SetActive(gameIsPaused);
                Time.timeScale = gameIsPaused ? 0 : 1;
            }
       }*/
        if (SceneManager.GetActiveScene()!=SceneManager.GetSceneByBuildIndex(0))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gameIsPaused = !gameIsPaused;
                pauseMenu.SetActive(gameIsPaused);
                Time.timeScale = gameIsPaused ? 0 : 1;
            }
        }

    }
}
