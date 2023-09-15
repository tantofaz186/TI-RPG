<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
=======
using System;
using UnityEngine;
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d

[System.Serializable]
public class Dialogue
{
<<<<<<< HEAD
    public string title;
	public Sprite image;
    [TextArea(3,10)]
    public string[] sentences;
=======
    public Dialog[] dialogues;
}
[Serializable]
public struct Dialog
{
    public string title;
    public Sprite image;
    [TextArea(3,10)]
    public string sentence;
>>>>>>> b1cf5f4cfcccc576c08b33b4b6f4a5fc498ee90d
}
