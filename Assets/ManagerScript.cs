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
        FirstValueInput,
        OperationInput,
        SecondValueInput,
        Output
    }
    private enum StepEqual
    {
        NoYInput,
        PreviousYInput
    }
    private Step step;
    private StepEqual stepEqual;
    public void Clear() 
    {
        x = default;
        y = default;
        step = Step.FirstValueInput;
        inputText.text = default;
        logText.text = default;
        stepEqual = StepEqual.NoYInput;
    }
    public void Type(string buttonName)
    {
        switch (stepEqual)

        {
            case StepEqual.NoYInput: 
                break;
            case StepEqual.PreviousYInput:
                step = Step.SecondValueInput;
                stepEqual = StepEqual.NoYInput;
                break;
        }
        inputText.text += buttonName;
    }
    
    public void SendCheckableOperation(Func<float,float,float?> callback, Func<float?, string> format) 
    {
        switch (step)
        {
            case Step.FirstValueInput:
                step = Step.SecondValueInput;
                x = Single.Parse(inputText.text);          
                logText.text = format(x);
                break;
            case Step.OperationInput:

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
               //step = Step.OperationInput;
                break;

            
        }
        
        operation = callback;
        operationFormat = format;
        inputText.text = default;
    }
    public void Equal()
    {
        switch (stepEqual)
        {
            case StepEqual.NoYInput:
                step = Step.FirstValueInput;
                stepEqual = StepEqual.PreviousYInput;
                y = Single.Parse(inputText.text);
                break;
            case StepEqual.PreviousYInput: 
                break; 
               
        }
            logText.text = operationFormat(x) + y.ToString() + "=";
            result = operation(x, y);
            x = result.Value;
            inputText.text = result.ToString();
            Debug.Log("Second step : x= " + x + "  " + "y= " + y);
    }
}
