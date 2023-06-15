using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

class Puzzle : Singleton<Puzzle>
{
    public void CompletarPuzzle()
    {
        bool completo = false;
        for(int i = 0; i < gameObject.transform.childCount-1; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<Pedestal>().Ativado == true)
            {
                Debug.Log(gameObject.transform.GetChild(i).name + " ativo.");
                completo = true;
            }
            else
            {
                completo = false;
                break;
            }
        }
        InvocarChave(completo);

    }

    void InvocarChave(bool quest)
    {
        if(quest==true)gameObject.transform.GetChild(5).gameObject.SetActive(true);
    }
    // Start is called before the first frame update

}
