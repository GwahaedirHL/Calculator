using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class DecimalPointButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ManagerScript manager;

    private void SendAction()
    {
        manager.ModifyInput(
            (strValue) =>
            {
                if (!strValue.Contains(","))
                    strValue = strValue + button.name;
                return strValue;
            },
            (flValue) =>
            {
                if (Math.Truncate(flValue) == flValue)
                    flValue = flValue * 0.1f;
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
