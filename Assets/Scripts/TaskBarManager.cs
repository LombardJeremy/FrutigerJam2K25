using UnityEngine;
using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine.Serialization;

public class TaskBarManager : MonoBehaviour
{
    public static TaskBarManager instance;

    [SerializeField] private GameObject iconPrefab;
    public List<TaskBarIconBehaviour> taskBarIconList = new List<TaskBarIconBehaviour>();
    public Transform taskBarIconGroupParent;
    
    public bool _isTaskBarUnlocked = false;
    private CanvasGroup _canvasGroup;

    [SerializeField] private int countToUnlock;
    
    [SerializeField] private TMP_Text date;

    public AudioClip sucessClip;
    public AudioClip tryClip;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateDate(-22);
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.interactable = false;
    }

    public void UnlockTaskBar()
    {
        countToUnlock--;
        if (countToUnlock > 0)
        {
            //PlayAnim
            GetComponent<RectTransform>().DOShakePosition( 1f, 4f, 15, 95f, true, true);
            GetComponent<SFXBehaviour>().PlaySFX(tryClip);
        }
        else
        {
            if (_isTaskBarUnlocked) return;
            GetComponent<SFXBehaviour>().PlaySFX(sucessClip);
            _isTaskBarUnlocked = true;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            GetComponent<RectTransform>().DOShakePosition( 2f, 20f, 40, 95f, true, true);
            GameManager.instance._taskBarUnlocked = true;
        }
    }

    public void CreateTaskBarIcon(WindowData window)
    {
        var tmpIcon = Instantiate(iconPrefab, taskBarIconGroupParent);
        var tmpIconBehaviour = tmpIcon.GetComponent<TaskBarIconBehaviour>();
        tmpIconBehaviour.parentWindow = window;
        tmpIconBehaviour.iconSprite = window.icon;
        tmpIconBehaviour.SetIconSprite();
        window.ownBehaviour.taskBarIcon =  tmpIconBehaviour;
        taskBarIconList.Add(tmpIconBehaviour);
    }
    
    void UpdateDate(int value)
    {
        DateTime now = DateTime.Now;
        now = now.AddYears(value);
        date.text = now.ToString("dd/MM/yyyy");
    }
}
