using System;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Dialog[] dialogues;
}
[Serializable]
public struct Dialog
{
    public string title;
    public Sprite image;
    [TextArea(3,10)]
    public string sentence;
}
