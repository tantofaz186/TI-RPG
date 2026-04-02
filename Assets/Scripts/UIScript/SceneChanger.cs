using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviourSingletonPersistent<SceneChanger>
{
    public void MudaCena(string cena) 
    {
        SceneManager.LoadScene(cena);
    }
}
