using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{

    public bool active = true;

    public UnityEvent onInteraction = new UnityEvent();

    public void OnInteraction()
    {
        print("Interacted !!");
        onInteraction.Invoke();
    }
}
