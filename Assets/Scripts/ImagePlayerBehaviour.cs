using System;
using UnityEngine;
using UnityEngine.UI;

public class ImagePlayerBehaviour : MonoBehaviour
{
    public Sprite spriteToUse;

    public void SetSprite()
    {
        if(spriteToUse != null)
            GetComponent<Image>().sprite = spriteToUse;
    }
}
