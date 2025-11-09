using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskBarIconBehaviour : MonoBehaviour
{
    public WindowData parentWindow;
    public Sprite iconSprite;
    
    public void IconIsClicked()
    {
        parentWindow.ownBehaviour.MinimizeWindow();
    }

    public void SetIconSprite()
    {
        if (iconSprite != null)
        {
            GetComponent<Image>().sprite = iconSprite;
        }
    }
}
