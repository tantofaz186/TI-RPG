using System;
using System.Collections.Generic;
using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(int.MinValue)]
public class DialogueManager : MonoBehaviourSingletonPersistent<DialogueManager>
{
    public TextMeshProUGUI dialogueTitle;
    public TextMeshProUGUI dialogueText;
    public Image dialogueImage;
    public GameObject dialoguePanel;
    public Action endDialogue;
    private Queue<Sprite> images;
    private Queue<string> sentences;
    private Queue<string> titles;

    private void Start()
    {
        sentences = new Queue<string>();
        images = new Queue<Sprite>();
        titles = new Queue<string>();
        dialoguePanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Time.timeScale = 0;
        dialoguePanel.SetActive(true);
        sentences.Clear();

        foreach (Dialog dialog in dialogue.dialogues)
        {
            sentences.Enqueue(dialog.sentence);
            images.Enqueue(dialog.image);
            titles.Enqueue(dialog.title);
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

        string nextTitle = titles.Dequeue();
        string nextSentence = sentences.Dequeue();
        Sprite nextImage = images.Dequeue();

        dialogueTitle.text = nextTitle;
        dialogueText.text = nextSentence;
        dialogueImage.sprite = nextImage;

        dialogueTitle.gameObject.SetActive(nextTitle != null);
        dialogueText.gameObject.SetActive(nextSentence != null);
        dialogueImage.gameObject.SetActive(nextImage != null);
    }

    private void EndDialogue()
    {
        Time.timeScale = 1;
        dialoguePanel.SetActive(false);
        endDialogue?.Invoke();
    }
}