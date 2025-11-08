using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskBarIconBehaviour : MonoBehaviour,
IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    public WindowBehaviour parentWindow;

    

    private void Start()
    {
        if(parentWindow == null) Destroy(this);
        parentWindow.SetIcon(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void IconIsClicked()
    {
        parentWindow.MinimizeWindow();
    }
}
