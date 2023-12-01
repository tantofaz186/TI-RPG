using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UrlButton : MonoBehaviour
{
    private string url;

    private void Awake()
    {
        url = GetComponent<TextMeshPro>().text;
        GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }
}