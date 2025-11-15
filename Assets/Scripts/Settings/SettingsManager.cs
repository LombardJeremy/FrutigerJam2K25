using DG.Tweening;
using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] Carousel carousel;

    bool isInCategory = false;

    [SerializeField] Categories categories;

    bool canChange = true;

    float posXCategories = 0f;
    
    public CanvasGroup canvasGroup;

    public AudioSource audioSource;

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
        if (GameManager.instance.currentGameState != GameState.Parameter) return;


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isInCategory) return;
            int index = carousel.GetSelectedIndex();
            carousel.Right();
            int diff = carousel.GetSelectedIndex() - index;

            if (diff != 0)
                audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isInCategory) return;
            int index = carousel.GetSelectedIndex();
            carousel.Left();
            int diff = carousel.GetSelectedIndex() - index;

            if (diff != 0)
                audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isInCategory) return;
            OnAppear();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isInCategory)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                    GameManager.instance.ChangeState(GameState.MainOS);

            }

            if (!isInCategory) return;
            OnHide();   

        }
    }

    void OnAppear()
    {
        if (!canChange) return;

        categories.loading = true;

        categories.HideOthers(carousel.GetSelectedIndex());

        canChange = false;
        isInCategory = true;
        categories.transform.DOLocalMoveX(300, 0.5f).SetEase(Ease.InOutCirc);
        carousel.transform.DOLocalMoveX(-300, 0.5f).SetEase(Ease.InOutCirc).OnComplete( () => { canChange = true; categories.loading = false; });
    }

    void OnHide()
    {
        if (!canChange) return;

        categories.loading = true;
        canChange = false;
        isInCategory = false;
        categories.transform.DOLocalMoveX(posXCategories, 0.5f).SetEase(Ease.InOutCirc);
        carousel.transform.DOLocalMoveX(100, 0.5f).SetEase(Ease.InOutCirc).OnComplete(() => { canChange = true; categories.loading = false; });
    }

}
