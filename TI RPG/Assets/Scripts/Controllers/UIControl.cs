using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviourSingletonPersistent<UIControl>
{
    public GameObject mainMenu;
    public GameObject skillTreeMenu;
    public GameObject dialogBox;
    public GameObject pauseMenu;
    public GameObject menuInventory;
    public GameObject painelInformativo;
    public GameObject medidores;
    public GameObject inventory;

    private bool inventoryOpen = true;

    private void Start()
    {
        ResetUI();
        SceneManager.sceneLoaded += ResetUI;
    }

    private void ResetUI(Scene arg0, LoadSceneMode arg1)
    {
        mainMenu.SetActive(arg0.buildIndex == 1); //Só ativar se estiver na cena do menu principal
        menuInventory.SetActive(arg0.buildIndex == 2);
        medidores.SetActive(arg0.buildIndex == 2);
        painelInformativo.SetActive(arg0.buildIndex == 2);
        skillTreeMenu.SetActive(false);
        dialogBox.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void ResetUI()
    {
        ResetUI(SceneManager.GetActiveScene(), LoadSceneMode.Single);
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

    public void OpenInventory()
    {
        inventory.SetActive(true);
        UpdateInventory();
    }

    public void CloseInventory()
    {
        UpdateInventory();
    }

    private void UpdateInventory()
    {
        if (inventoryOpen)
        {
            menuInventory.GetComponent<Animator>().SetTrigger("fechar");
            StartCoroutine(liberarMudanca());
        }
        else
        {
            menuInventory.GetComponent<Animator>().SetTrigger("abrir");
        }

        inventoryOpen = !inventoryOpen;
    }

    private IEnumerator liberarMudanca()
    {
        yield return new WaitForSeconds(1.0f);
        inventory.SetActive(false);
    }
}