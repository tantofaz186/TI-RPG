using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeShow : MonoBehaviour
{
    public GameObject menuUI;
    private bool isSkillTree;
    public GameObject[] esconderUI;
    private void Start()
    {
        isSkillTree = false;
        menuUI.SetActive(false);
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.S))
    //     {
    //         if (isSkillTree)
    //         {
    //             FecharSkillTree();
    //         }
    //         else
    //         {
    //             AbrirSkillTree();
    //         }
    //     }
    // }

    private void AbrirSkillTree()
    {
        for (int i = 0; i < esconderUI.Length; i++)
        {
            esconderUI[i].SetActive(false);

        }
        isSkillTree = true;
        menuUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game when the menu is open
    }

    private void FecharSkillTree()
    {
        for (int i = 1; i < esconderUI.Length; i++)
        {
            esconderUI[i].SetActive(true);
        }
        isSkillTree = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game when the menu is closed
    }
}
