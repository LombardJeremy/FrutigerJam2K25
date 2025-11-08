using UnityEngine;
using System;
using TMPro;


public class TaskBarBehaivour : MonoBehaviour
{
    [SerializeField] private TMP_Text date;
    void Start()
    {
        UpdateDate();
    }
    
    void UpdateDate()
    {
        DateTime now = DateTime.Now;
        now = now.AddYears(-22);
        date.text = now.ToString("dd/MM/yyyy");
    }
}
