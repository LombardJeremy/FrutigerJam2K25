using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Serialization;

public class TaskBarManager : MonoBehaviour
{
    public static TaskBarManager instance;

    [SerializeField] private GameObject iconPrefab;
    public List<TaskBarIconBehaviour> taskBarIconList = new List<TaskBarIconBehaviour>();
    public Transform taskBarIconGroupParent;
    
    [SerializeField] private TMP_Text date;
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
        UpdateDate();
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
        Debug.Log("Instantiated TaskBarIcon");
    }
    
    void UpdateDate()
    {
        DateTime now = DateTime.Now;
        now = now.AddYears(-22);
        date.text = now.ToString("dd/MM/yyyy");
    }
}
