using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    private VideoPlayer player;

    private void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.loopPointReached += ChangeSceneWhenVideoEnds;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnDisable()
    {
        player.loopPointReached -= ChangeSceneWhenVideoEnds;
    }

    private void ChangeSceneWhenVideoEnds(VideoPlayer source)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}