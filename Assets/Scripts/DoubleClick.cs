using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class DoubleClick : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("Temps maximum entre deux clics pour être considéré comme un double-clic (en secondes)")]
    public float doubleClickThreshold = 0.3f;

    [Tooltip("Événement déclenché quand le bouton est double-cliqué")]
    public UnityEvent onDoubleClick;

    private float lastClickTime = -1f;

    public void OnPointerClick(PointerEventData eventData)
    {
        float time = Time.time;

        // Si le deuxième clic arrive dans la fenêtre de temps
        if (time - lastClickTime <= doubleClickThreshold)
        {
            // Reset pour éviter les triples clics
            lastClickTime = -1f;
            OnDoubleClick();
        }
        else
        {
            // Premier clic détecté
            lastClickTime = time;
        }
    }

    public void OnDoubleClick()
    {
        // Animation dotween ?
        onDoubleClick?.Invoke();
    }
}