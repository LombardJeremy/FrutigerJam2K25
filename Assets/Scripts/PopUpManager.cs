using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public TMP_Text NamePlace;
    public string Name;
    public Sprite icon;
    public Image iconPlace;
    public TMP_Text MainContentPlace;
    public string MainContent;
    public TMP_Text SecondContentPlace;
    public string SecondContent;
    private void Start()
    {
        //SetText
        NamePlace.text = Name;
        MainContentPlace.text = MainContent;
        SecondContentPlace.text = SecondContent;
        //SetIcon
        iconPlace.sprite = icon;
        //Play Animation
    }

    public void OpenWindowAnimation()
    {
        
    }
}
