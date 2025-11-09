using System;
using UnityEngine;

public class WindowData : MonoBehaviour
{
    public Sprite icon;
    public string nameOfWindow;
    public WindowBehaviour ownBehaviour;
    public bool isFocus = false;
    
    public AudioClip clip;
    public Sprite image;

    private void Start()
    {
        Debug.Log(nameOfWindow);
        IsFocusOn();
        TaskBarManager.instance.CreateTaskBarIcon(this);
    }

    public void IsFocusOn()
    {
        WindowData[] allWindows = FindObjectsOfType<WindowData>();
        foreach (WindowData window in allWindows)
        {
            if (window == this) continue;
            window.isFocus = false;
        }
        isFocus = true;
        transform.SetAsLastSibling();
    }
}
