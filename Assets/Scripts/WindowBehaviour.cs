using UnityEngine;
using UnityEngine.EventSystems;

public class WindowBehaviour : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] public RectTransform mainParentTransform;
    private Vector2 offset;
    public Vector2 lastPosBeforeClosed;
    public bool isWindowOpen = true;
    
    public TaskBarIconBehaviour taskBarIcon;
    
    public int id;

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
        if (taskBarIcon != null)
        {
            Destroy(taskBarIcon.gameObject);
        }
        Destroy(mainParentTransform.gameObject);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
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
