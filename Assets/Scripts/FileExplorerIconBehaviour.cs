using System;
using UnityEngine;
using UnityEngine.UI;

public class FileExplorerIconBehaviour : MonoBehaviour
{
    public Sprite icon;
    
    public TypeOfFile typeOfFile;
    
    private WindowController _windowController;
    
    public AudioClip audioClip;
    public Sprite sprite;

    private void Start()
    {
        GetComponent<Image>().sprite = icon;
        _windowController = GetComponentInParent<WindowController>();
    }

    public void OnClick()
    {
        if (typeOfFile == TypeOfFile.FolderImage)
        {
            GameObject child = Instantiate(_windowController.windowExplorerImagePrefab, _windowController._data.ownBehaviour.canvas.transform);
            child.transform.position = new Vector3(child.transform.position.x + 15, child.transform.position.y + 10, child.transform.position.z);
        } else if (typeOfFile == TypeOfFile.FolderMusic)
        {
            GameObject child = Instantiate(_windowController.windowExplorerMusicPrefab, _windowController._data.ownBehaviour.canvas.transform);
            child.transform.position = new Vector3(child.transform.position.x + 15, child.transform.position.y + 10, child.transform.position.z);
        } else if (typeOfFile == TypeOfFile.Music)
        {
            GameObject child = Instantiate(_windowController.musicReader, _windowController._data.ownBehaviour.canvas.transform);
            if (audioClip != null)
                child.GetComponent<WindowData>().clip = audioClip;
        } else if (typeOfFile == TypeOfFile.Image)
        {
            GameObject child = Instantiate(_windowController.imageReader, _windowController._data.ownBehaviour.canvas.transform);
            if (sprite != null)
                child.GetComponent<WindowData>().image = sprite;
        }
    }
}

public enum TypeOfFile
{
    FolderMusic,
    FolderImage,
    Music,
    Image
}
