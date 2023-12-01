using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UrlButton : MonoBehaviour
{
    private string url;

    private void Awake()
    {
        url = GetComponentInChildren<TMP_Text>().text;
        GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(url));
    }
}