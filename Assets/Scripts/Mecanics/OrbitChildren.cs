using UnityEngine;

public class OrbitChildren : MonoBehaviour
{
    [SerializeField] private float radius = 3f;           // Distance des enfants au centre
    [SerializeField] private float rotationSpeed = 50f;   // Vitesse de rotation en degrés/s
    [SerializeField] private bool autoArrange = true;     // Si true, place les enfants en cercle automatiquement

    private Transform[] children;
    private float[] angles;

    void Start()
    {
        NewChildren();
    }

    public void NewChildren()
    {
        int count = transform.childCount;
        children = new Transform[count];
        angles = new float[count];

        for (int i = 0; i < count; i++)
        {
            children[i] = transform.GetChild(i);
            angles[i] = i * (360f / count); // Espace uniforme en degrés
        }

        if (autoArrange)
            ArrangeChildrenInCircle();
    }

    void Update()
    {
        for (int i = 0; i < children.Length; i++)
        {
            // Fait tourner l’angle au fil du temps
            angles[i] += rotationSpeed * Time.deltaTime;
            if (angles[i] > 360f) angles[i] -= 360f;

            // Calcule la nouvelle position (ici en 2D sur X/Y)
            float rad = angles[i] * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;

            children[i].localPosition = pos;
        }
    }

    private void ArrangeChildrenInCircle()
    {
        for (int i = 0; i < children.Length; i++)
        {
            float rad = angles[i] * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0) * radius;
            children[i].localPosition = pos;
        }
    }
}