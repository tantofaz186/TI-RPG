using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviourSingletonPersistent<UIControl> {

    //Carrega uma cena
    public void MudarCena(string nomeCena)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomeCena);
    }

    public void Sair()
    {
        Application.Quit(0);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void RestartScene()
    {
        MudarCena(GetSceneName());
    }
}
