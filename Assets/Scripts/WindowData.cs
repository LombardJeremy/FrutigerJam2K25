using System;
using UnityEngine;

public class WindowData : MonoBehaviour
{
    public Sprite icon;
    public string nameOfWindow;
    public WindowBehaviour ownBehaviour;
    public bool isFocus = false;

    private void Start()
    {
        Debug.Log(nameOfWindow);
        TaskBarManager.instance.CreateTaskBarIcon(this);
    }
}
