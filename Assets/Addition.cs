using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Addition : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private ManagerScript manager;

    private void SendAction()
    {
        manager.SendCheckableOperation(
            (x, y) =>
            {
                return x + y;
            },
            (result) =>
            {
                return result + " " + button.name;
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
