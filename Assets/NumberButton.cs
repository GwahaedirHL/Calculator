using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberButton : MonoBehaviour
{
    [SerializeField] private ManagerScript manager;
    [SerializeField] private Button button;
    private void OnEnable()
    {
        button.onClick.AddListener(() => manager.Type(button.name)); 
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners(); 
    }
}
