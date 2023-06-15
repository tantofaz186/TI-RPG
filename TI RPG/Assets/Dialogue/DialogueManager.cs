using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(int.MinValue)]
public class DialogueManager : Singleton<DialogueManager>
{
    public TextMeshProUGUI dialogueTitle;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;
    public GameObject dialoguePanel;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        Debug.Log("in");
        dialoguePanel.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    private void EndDialogue()
    {
        Time.timeScale = 1;
        dialoguePanel.SetActive(false);
    }
}
