using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EqualButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ManagerScript manager;
    private void Reset()
    {
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(()=> manager.Equal());
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
