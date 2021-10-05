using System;
using UnityEngine;
using UnityEngine.UI;
public enum Step
{
    Opening,
    ZeroDivision,
    FirstValueInput,
    OperationInput,
    SecondValueInput,
    UsingCache
}
public class Calculator 
{
    private float x;
    private float y;
    private float? result;
    private float? cache;

    private Step step;

    public Step Step => step;
    public float X => x;
    public void Clear()
    {
        x = 0;
        y = default;
        cache = default;
        step = Step.Opening;
    }
    public void Type(string digit) 
    {
        switch (step)

        {
            case Step.Opening:
                x = float.Parse(digit);
                step = Step.FirstValueInput;
                break;
            case Step.FirstValueInput:
                x = float.Parse(x.ToString() + digit);
                break;
            case Step.SecondValueInput:
               
                break;
            case Step.OperationInput:
               
                step = Step.SecondValueInput;
                break;
            case Step.ZeroDivision:
               
                step = Step.FirstValueInput;
                break;
            case Step.UsingCache:
                //TODO!
                break;
        }

    }

}

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private Text inputText;
    [SerializeField] private Text logText;
    private float x;
    private float y;
    private float? result;
    private float? cache;
    private Func<float, float, float?> operation;
    private Func<float?, string> operationFormat;
    Calculator calculator;
    private enum Step 
    {
        Opening,
        ZeroDivision,
        FirstValueInput,
        OperationInput,
        SecondValueInput,
        UsingCache
    }
    private Step step;
    private void Start()
    {
        calculator = new Calculator();
        Clear();
    }
    private void Update()
    {
        Debug.LogWarning(calculator.Step.ToString());
    }
    public void Clear() 
    {
        calculator.Clear();
        inputText.text = "0";
        logText.text = default;
    }
    public void Type(string buttonName)
    {
        calculator.Type(buttonName);
        inputText.text = calculator.X.ToString();
        //switch (step)

        //{
        //    case Step.Opening:
        //        inputText.text = buttonName;
        //        step = Step.FirstValueInput;
        //        break;
        //    case Step.FirstValueInput:
        //        inputText.text += buttonName;
        //        break;
        //    case Step.SecondValueInput:
        //        inputText.text += buttonName;
        //        break;
        //    case Step.OperationInput:
        //        inputText.text = buttonName;
        //        step = Step.SecondValueInput;
        //        break;
        //    case Step.ZeroDivision:
        //        inputText.text = buttonName;
        //        logText.text = default;
        //        step = Step.FirstValueInput;
        //        break;
        //    case Step.UsingCache:
        //       //TODO!
        //        break;
        //}
        
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
                x = Single.Parse(inputText.text);          
                logText.text = format(x);
                operation = callback;
                operationFormat = format;
                inputText.text = default;
                step = Step.OperationInput;
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
                {
                    inputText.text = "На ноль делить нельзя!";
                    step = Step.ZeroDivision;
                }
                else
                {
                    result = tmp;
                    x = result.Value;
                    logText.text = format(x);
                    inputText.text = x.ToString(); 
                    step = Step.OperationInput;
                }
                operation = callback;
                operationFormat = format;
                break;
            case Step.ZeroDivision:
                break;
            case Step.UsingCache:
                operation = callback;
                operationFormat = format;
                x = Single.Parse(inputText.text);
                logText.text = operationFormat(x);
                step = Step.OperationInput;
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
            case Step.FirstValueInput:
                logText.text = inputText.text + "=";
                x = Single.Parse(inputText.text);
                step = Step.Opening;
                break;
            case Step.OperationInput:
                var tmp = operation(x, x);
                if (tmp == null)
                {
                    inputText.text = "Результат не определен!";
                    step = Step.ZeroDivision;
                }
                else
                {
                    logText.text = operationFormat(x) + x.ToString() + "=";
                    inputText.text = tmp.ToString();
                    cache = x;
                    step = Step.UsingCache;
                }   
                break;
            case Step.SecondValueInput:
                y = Single.Parse(inputText.text);
                logText.text = operationFormat(x);
                var tmp1 = operation(x, y);
                if (tmp1 == null)
                {
                    inputText.text = "Делить на ноль нельзя!";
                    step = Step.ZeroDivision;
                }
                else
                {
                    x = tmp1.Value;
                    cache = y;
                    logText.text += y.ToString() + "=";
                    inputText.text = tmp1.ToString();
                    step = Step.UsingCache;
                }
                break;
            case Step.UsingCache:
                x = Single.Parse(inputText.text);
                var tmp2 = operation(x, cache.Value);
                logText.text = operationFormat(x) + cache.ToString() + "=";
                inputText.text = tmp2.ToString();
                break;
            case Step.ZeroDivision:
                Clear();
                break; 
               
        }
        
        Debug.Log("Second step : x= " + x + "  " + "y= " + y + " cache= " + cache.ToString());  
    }
}
