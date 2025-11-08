using UnityEngine;
using UnityEngine.Events;

public class Interactible : MonoBehaviour
{

    public UnityEvent onInteraction = new UnityEvent();

    public void OnInteraction()
    {
        print("Interacted !!");
        onInteraction.Invoke();
    }
}
