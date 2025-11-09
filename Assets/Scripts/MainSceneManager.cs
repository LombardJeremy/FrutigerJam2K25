using System;
using DG.Tweening;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    
    void Start()
    {
        GameManager.instance.OnGameStateChanged.AddListener(OnGameStateChanged);
        canvasGroup =  GetComponent<CanvasGroup>();
        canvas = canvasGroup.GetComponent<Canvas>();
    }

    private void OnGameStateChanged(GameState newState)
    {
        if (newState == GameState.MainOS)
        {
            canvasGroup.DOFade(1, 1);
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        if (newState == GameState.Parameter)
        {
            canvasGroup.DOFade(0, 1);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged.RemoveListener(OnGameStateChanged);
    }
}
