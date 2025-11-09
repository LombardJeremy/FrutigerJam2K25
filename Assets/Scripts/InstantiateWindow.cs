using UnityEngine;

public class InstantiateWindow : MonoBehaviour
{
    public GameObject prefab;
    public GameObject currentWindowOpened;
    public Transform parent;

    public void CreateWindow()
    {
        if (currentWindowOpened != null) return;
        currentWindowOpened = Instantiate(prefab, parent);
    }
}
