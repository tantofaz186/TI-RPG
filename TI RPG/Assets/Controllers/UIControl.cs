using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public string nomeCena;

    //Carrega uma cena
    public void MudarCena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nomeCena);
    }

    public void Sair()
    {
        Application.Quit(0);
    }
}
