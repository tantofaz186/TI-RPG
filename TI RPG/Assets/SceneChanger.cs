using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public void MudaCena(string cena) 
    {
        SceneManager.LoadScene(cena);
    }
}
