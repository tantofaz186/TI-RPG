using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MedidoresPlayer : MonoBehaviour
{
    public string nomeScript;
    public string nomeVariavel;
    public Slider medidor;
    private Type componentType;
    private FieldInfo fieldInfo;

    private Component foundComponent;

    private void Start()
    {
        AcharScriptPorNome(nomeScript, nomeVariavel);
    }

    private void Update()
    {
        if (fieldInfo != null) medidor.value = (float)fieldInfo.GetValue(foundComponent);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += DesativaNoMenu;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= DesativaNoMenu;
    }

    private void AcharScriptPorNome(string scriptNome, string variavelNome)
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

    private void DesativaNoMenu(Scene arg0, LoadSceneMode loadSceneMode)
    {
        medidor.gameObject.SetActive(arg0.buildIndex != 0);
    }
}