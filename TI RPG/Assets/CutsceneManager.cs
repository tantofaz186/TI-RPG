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

    private void ChangeSceneWhenVideoEnds(VideoPlayer source)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}