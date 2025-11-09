using DG.Tweening;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] Carousel carousel;

    bool isInCategory = false;

    [SerializeField] Categories categories;

    bool canChange = true;

    float posXCategories = 0f;
    
    public CanvasGroup canvasGroup;

    void Start()
    {
        posXCategories = categories.transform.localPosition.x;
        
        GameManager.instance.OnGameStateChanged.AddListener(OnGameStateChanged);
    }
    
    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.Parameter)
        {
            canvasGroup.DOFade(1, 1);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        if (newState == GameState.MainOS)
        {
            canvasGroup.DOFade(0, 1);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isInCategory) return;
            carousel.Right();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isInCategory) return;
            carousel.Left();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isInCategory) return;
            OnAppear();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (!isInCategory) return;
            OnHide();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInCategory) return;
            GameManager.instance.ChangeState(GameState.MainOS);
        }


    }

    void OnAppear()
    {
        if (!canChange) return;

        categories.HideOthers(carousel.GetSelectedIndex());

        canChange = false;
        isInCategory = true;
        categories.transform.DOLocalMoveX(300, 0.5f).SetEase(Ease.InOutCirc);
        carousel.transform.DOLocalMoveX(-300, 0.5f).SetEase(Ease.InOutCirc).OnComplete( () => { canChange = true; });
    }

    void OnHide()
    {
        if (!canChange) return;

        canChange = false;
        isInCategory = false;
        categories.transform.DOLocalMoveX(posXCategories, 0.5f).SetEase(Ease.InOutCirc);
        carousel.transform.DOLocalMoveX(100, 0.5f).SetEase(Ease.InOutCirc).OnComplete(() => { canChange = true; });
    }

}
