using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIControl : MonoBehaviourSingletonPersistent<UIControl> {

    public GameObject mainMenu;
    public GameObject skillTreeMenu;
    public GameObject dialogBox;
    public GameObject pauseMenu;
    public GameObject quickInventory;
    public GameObject inventory;

    private void Start()
    {
        ResetUI();
        SceneManager.sceneLoaded += ResetUI;

    }

    private void ResetUI(Scene arg0, LoadSceneMode arg1)
    {
        mainMenu.SetActive(arg0.buildIndex == 0); //Só ativar se estiver na cena do menu principal
        inventory.SetActive(arg0.buildIndex != 0);
        quickInventory.SetActive(arg0.buildIndex != 0);
        skillTreeMenu.SetActive(false);
        dialogBox.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void ResetUI()
    {
        ResetUI(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    public void AbrirSkillTree()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByBuildIndex(0))//Ignorar caso estiver na Cena Menu
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (skillTreeMenu.activeSelf == false)
                {
                    skillTreeMenu.SetActive(true);
                }

                else
                {
                    skillTreeMenu.SetActive(false);
                }


            }
        }
    } 
    public void SetInventoryActive(bool active)
    {
        inventory.SetActive(active);
    }
    private void Update()
    {
        AbrirSkillTree();
    }


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
