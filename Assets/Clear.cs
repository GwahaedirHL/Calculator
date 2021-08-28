using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Clear : MonoBehaviour
{
    [SerializeField] private Button clearButton;
    [SerializeField] private ManagerScript manager;
    private void SendAction()
    {
        manager.Clear();
    }
    public void OnEnable()
    {
        clearButton.onClick.AddListener(SendAction);
    }
    public void OnDisable()
    {
        clearButton.onClick.RemoveAllListeners();
    }
}
