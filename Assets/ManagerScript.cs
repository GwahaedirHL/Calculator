using System;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private Text inputText;
    [SerializeField] private Text logText;
    private float x;
    private float y;
    private float? result;
    private Func<float, float, float?> operation;
    private Func<float?, string> operationFormat; 
    private enum Step 
    {
        Opening,
        ZeroDivision,
        FirstValueInput,
        OperationInput,
        SecondValueInput,
        Output,
        NoYInput,
        PreviousYInput
    }
    private Step step;
    private void Start()
    {
        Clear();
    }
    public void Clear() 
    {
        x = 0;
        y = default;
        step = Step.Opening;
        inputText.text = "0";
        logText.text = default;
    }
    public void Type(string buttonName)
    {
        switch (step)

        {
            case Step.Opening:
                inputText.text = buttonName;
                step = Step.FirstValueInput;
                break;
            case Step.FirstValueInput:
                inputText.text += buttonName;
                break;
            case Step.NoYInput:
                inputText.text += buttonName;
                break;
            case Step.PreviousYInput:
                step = Step.SecondValueInput;
                step = Step.NoYInput;
                inputText.text += buttonName;
                break;
        }
        
    }
    
    public void SendCheckableOperation(Func<float,float,float?> callback, Func<float?, string> format) 
    {
        switch (step)
        {
            case Step.Opening:

                logText.text = format(x);
                operation = callback;
                operationFormat = format;
                step = Step.OperationInput;
                break;
            case Step.FirstValueInput:
                step = Step.SecondValueInput;
                x = Single.Parse(inputText.text);          
                logText.text = format(x);
                operation = callback;
                operationFormat = format;
                inputText.text = default;
                break;
            case Step.OperationInput:
                logText.text = format(x);
                operation = callback;
                operationFormat = format;

                break;
            case Step.SecondValueInput:
                y = Single.Parse(inputText.text);           
                var tmp = operation(x, y);
                if (tmp == null)
                    logText.text = format(tmp);
                else
                {
                    result = tmp;
                    x = result.Value;
                    logText.text = format(x);
                }
                operation = callback;
                operationFormat = format;
                inputText.text = default;
                //step = Step.OperationInput;
                break;

            
        }
        
        
    }
    public void Equal()
    {
        switch (step)
        {
            case Step.Opening:
                logText.text = inputText.text + "=";
                break;
            case Step.NoYInput:
                step = Step.FirstValueInput;
                step = Step.PreviousYInput;
                y = Single.Parse(inputText.text);
                logText.text = operationFormat(x);
                var tmp = operation(x, y);
                if (tmp == null)
                {
                    Clear();
                    inputText.text = "Делить на ноль нельзя!";
                }
                else
                {
                    result = tmp;
                    x = result.Value;
                    logText.text += y.ToString() + "=";
                    inputText.text = result.ToString();
                }
                break;
            case Step.PreviousYInput: 
                break; 
               
        }
        
        Debug.Log("Second step : x= " + x + "  " + "y= " + y);  
    }
}
