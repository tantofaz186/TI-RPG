using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MedidoresPlayer : MonoBehaviour
{
    public string componentNameToFind; // Name of the component to search for
    public string variableNameToAccess; 
    //[SerializeField]private float valorVariavel;
    public Slider medidor; // Reference to your Slider component in the Inspector.

    private Component foundComponent;
    private System.Type componentType;
    private  System.Reflection.FieldInfo fieldInfo;

    void Start()
    {
        FindComponentsByName(componentNameToFind, variableNameToAccess);
    }


    void Update()
    {
        DesativaNoMenu();
        if(foundComponent==null){
        FindComponentsByName(componentNameToFind, variableNameToAccess);
        }
        if (fieldInfo != null)
        {
            medidor.value=(float)fieldInfo.GetValue(foundComponent);
        }
        else
        {
            Debug.LogWarning("Component does not have a variable named " + variableNameToAccess);
        }
    }

    void FindComponentsByName(string componentName, string variableName)
    {
        GameObject[] allGameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject go in allGameObjects)
        {
            foundComponent = go.GetComponent(componentName);

            if (foundComponent != null)
            {
                componentType = foundComponent.GetType();
                fieldInfo = componentType.GetField(variableNameToAccess);
                Debug.Log("Found component with name: " + componentName);
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