using System.Collections.Generic;
using Refactor.Scripts.Quest;
using UnityEngine;

public class Pedestal : MonoBehaviour
{
    public Quest quest;

    public Material materialOn;
    public Material materialOff;
    public bool Ativado { get; private set; }

    private void Start()
    {
        transform.GetChild(1).GetComponent<MeshRenderer>().material = materialOff;
        transform.GetChild(2).GetComponent<MeshRenderer>().material = materialOff;


        quest.OnComplete += OnQuestComplete;
    }

    private void OnDisable()
    {
        quest.OnComplete -= OnQuestComplete;
    }

    private void OnQuestComplete(List<Rewards> obj)
    {
        transform.GetChild(1).GetComponent<MeshRenderer>().material = materialOn;
        transform.GetChild(2).GetComponent<MeshRenderer>().material = materialOn;
        Ativado = true;
    }
}