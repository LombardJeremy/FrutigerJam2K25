using TMPro;
using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    public string mainText;
    public TMP_Text tmpText;
    void Start()
    {
        SetText();
    }

    public void SetText()
    {
        tmpText.text = mainText;
    }

    public void AddText(string text)
    {
        mainText += text;
        SetText();
    }
}
