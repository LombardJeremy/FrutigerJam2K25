using System.Collections.Generic;
using UnityEngine;

public class InteractibleManager : MonoBehaviour
{

    public List<Interactible> interactibles;

    public bool IsCollidingWithElements(Vector2 position)
    {
        foreach (Interactible interactible in  interactibles)
        {
            if (IsInElement(interactible, position))
            {
                interactible.OnInteraction();
                return true;
            }
        }
        return false;
    }

    public bool IsInElement(Interactible interactible, Vector2 position)
    {
        if (interactible == null) return false;

        RectTransform rectTransform = interactible.GetComponent<RectTransform>();
        if (rectTransform == null) return false;

        Rect rect = rectTransform.rect;
        Canvas canvas = rectTransform.GetComponentInParent<Canvas>();
        return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, position, canvas.worldCamera);
    }

}
