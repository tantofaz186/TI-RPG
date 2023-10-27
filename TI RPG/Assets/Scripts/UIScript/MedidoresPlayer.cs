using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MedidoresPlayer : MonoBehaviour
{
    public string nomeScript; 
    public string nomeVariavel; 
    public Slider medidor; 

    private Component foundComponent;
    private System.Type componentType;
    private  System.Reflection.FieldInfo fieldInfo;

    void Start()
    {
        AcharScriptPorNome(nomeScript, nomeVariavel);
    }


    void Update()
    {
        DesativaNoMenu();
        if(foundComponent==null){
        AcharScriptPorNome(nomeScript, nomeVariavel);
        }
        if (fieldInfo != null)
        {
            medidor.value=(float)fieldInfo.GetValue(foundComponent);
        }
    }

    void AcharScriptPorNome(string scriptNome, string variavelNome)
    {
        GameObject[] TodosOsGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject go in TodosOsGameObjects)
        {
            foundComponent = go.GetComponent(scriptNome);

            if (foundComponent != null)
            {
                componentType = foundComponent.GetType();
                fieldInfo = componentType.GetField(variavelNome);
                break; 
            }
        }
    }
    void DesativaNoMenu(){
        if(SceneManager.GetActiveScene().name=="Menu"){
        medidor.gameObject.SetActive(false);
        }else{
          medidor.gameObject.SetActive(true);   
        }
    }
}