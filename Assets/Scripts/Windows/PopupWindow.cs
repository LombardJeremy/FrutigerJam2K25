using DG.Tweening;
using TMPro;
using UnityEngine;

public class PopupWindow : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI textMesh;
    [SerializeField] Transform popup;

    public static PopupWindow Instance;

    bool inAnim = false;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(this);
    }


    public void SetPopup(string text)
    {
        if (inAnim) return;
        inAnim = true;
        textMesh.text = text;
        popup.DOLocalMoveX(-150, 1f).SetEase(Ease.InOutCirc);

        popup.DOLocalMoveX(200, 1f).SetEase(Ease.InOutCirc).SetDelay(5f).OnComplete( () => { inAnim = false; });

    }

}
