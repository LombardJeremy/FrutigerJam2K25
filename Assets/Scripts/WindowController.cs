using UnityEngine;
using UnityEngine.UI;

public class WindowController : MonoBehaviour
{
    [SerializeField] private WindowData _data;
    [SerializeField] int horizontalElements = 1;
    [SerializeField] int verticalElements = 1;

    int numElements = 1;

    int indexHorizontal = 0;
    int indexVertical = 0;

    int lastIndexHorizontal = 0;
    int lastIndexVertical = 0;

    void Start()
    {
        numElements = transform.childCount;
        transform.GetChild(indexHorizontal + indexVertical).GetComponent<Image>().color = Color.red;
    }

    void Update()
    {

        HandleInputs();

    }

    void HandleInputs()
    {

        if (!_data.isFocus) return;

        lastIndexHorizontal = indexHorizontal;
        lastIndexVertical = indexVertical;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            indexHorizontal -= 1;
            ClampValues();
            ChangeColors();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            indexHorizontal += 1;
            ClampValues();
            ChangeColors();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            indexVertical -= 1;
            ClampValues();
            ChangeColors();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            indexVertical += 1;
            ClampValues();
            ChangeColors();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            transform.GetChild(indexHorizontal + indexVertical * horizontalElements).GetComponent<DoubleClick>().OnDoubleClick();
        }
    }

    void ClampValues()
    {
        if (indexHorizontal < 0) indexHorizontal = 0;
        if (indexHorizontal > horizontalElements-1) indexHorizontal = horizontalElements - 1;

        if (indexVertical < 0) indexVertical = 0;
        if (indexVertical > verticalElements-1) indexVertical = verticalElements - 1;

        if (indexHorizontal + indexVertical * horizontalElements > numElements-1) indexHorizontal = numElements % horizontalElements - 1;
    }

    void ChangeColors()
    {
        if (indexVertical != lastIndexVertical || indexHorizontal != lastIndexHorizontal)
        {
            transform.GetChild(indexHorizontal + indexVertical * horizontalElements).GetComponent<Image>().color = Color.red;
            transform.GetChild(lastIndexHorizontal + lastIndexVertical * horizontalElements).GetComponent<Image>().color = Color.gray;
        }
    }
}
