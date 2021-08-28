using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Division : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ManagerScript manager;

    private void SendAction() 
    {
        manager.SendCheckableOperation(
            (x, y) =>
            {
                if (y == 0)
                    return null;
                return x / y;
                
            },
            (result) => 
            {
                if (result == null)
                {
                    manager.Clear();
                    return "Делить на ноль нельзя!";
                }
                return result.ToString() + " " + button.name;
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
