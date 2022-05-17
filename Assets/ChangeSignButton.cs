using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class ChangeSignButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ManagerScript manager;

    private void SendAction()
    {
        manager.ModifyInput(
            (strValue) =>
            {
                return strValue;
            },
            (flValue) =>
            {
                flValue = flValue * -1;
                return flValue;
            }
            );
    }
    private void Reset()
    {
        button = GetComponent<Button>();
    }
    private void OnEnable()
    {
        button.onClick.AddListener(SendAction);
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
