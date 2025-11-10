using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class WindowBehaviour : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] public Canvas canvas;
    [SerializeField] public RectTransform mainParentTransform;
    private Vector2 offset;
    public Vector2 lastPosBeforeClosed;
    public bool isWindowOpen = true;
    
    private bool isclosing = false;
    public TaskBarIconBehaviour taskBarIcon;

    private void Start()
    {
        canvas = transform.GetComponentInParent<Canvas>();
    }

    public void MinimizeWindow()
    {
        if (isWindowOpen)
        {
            lastPosBeforeClosed = mainParentTransform.localPosition;
            mainParentTransform.localPosition = new Vector2(10000f,10000f); //Pos to set not good
            isWindowOpen =  false;
        }
        else
        {
            mainParentTransform.localPosition = lastPosBeforeClosed;
            isWindowOpen = true;
        }
    }

    public void CloseWindow()
    {
        if (!isclosing)
        {
            StartCoroutine(CloseAnimation());
            isclosing  = true;
        }
    }

    IEnumerator CloseAnimation()
    {
        mainParentTransform.DOScale(0f, 0.5f).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(0.7f);
        if (taskBarIcon != null)
        {
            Destroy(taskBarIcon.gameObject);
        }
        Destroy(mainParentTransform.gameObject);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        mainParentTransform.GetComponent<WindowData>().IsFocusOn();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );
        offset = mainParentTransform.localPosition - (Vector3)localPoint;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        mainParentTransform.localPosition = localPoint + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
