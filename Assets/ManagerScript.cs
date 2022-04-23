using System;
using UnityEngine;
using UnityEngine.UI;
public interface ICalculator : IType, ISendCheckableOperation, IEqual
{
    ICalculator ChangeState();
}
public class ManagerScript : MonoBehaviour
{
    [SerializeField] private Text inputText;
    [SerializeField] private Text logText;
    Calculator calculator;
    private Func<string, string> operationFormat;


    ICalculator icalculator;

    private void Start()
    {
        calculator = new Calculator();
        icalculator = new StartingState(calculator);
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
        icalculator.TextParse(buttonName);
        icalculator.ChangeState();

        calculator.Type(buttonName);
        switch (calculator.Step)
        {
            case Step.Opening:
            case Step.FirstValueInput:
                inputText.text = calculator.X;
                break;
            case Step.SecondValueInput:
            case Step.OperationInput:
                inputText.text = calculator.Y;
                break;
            case Step.ZeroDivision:
                inputText.text = calculator.X;
                logText.text = default;
                break;
            case Step.ResultOperation:                
                break;
        }
    }
    
    public void SendCheckableOperation(Func<float,float,float?> callback, Func<string, string> format) 
    {
        icalculator.OperationTypeSaving(callback);
        icalculator.ChangeState();

        calculator.SendCheckableOperation(callback);
        switch (calculator.Step)
        {
            case Step.Opening:
                logText.text = format(calculator.X);
                operationFormat = format;
                break;
            case Step.FirstValueInput:
                logText.text = format(calculator.X);
                operationFormat = format;
                inputText.text = default;                
                break;
            case Step.OperationInput:
                logText.text = format(calculator.X);
                inputText.text = calculator.X.ToString();
                operationFormat = format;
                break;  
            case Step.ZeroDivision:
                logText.text = operationFormat(calculator.X) + format(calculator.Y);
                inputText.text = "На ноль делить нельзя!";
                break;
        }


    }
    public void Equal()
    {
        icalculator.ActivateOperation();
        icalculator.ChangeState();

        calculator.Equal();
        switch (calculator.Step)
        {
            case Step.Opening:
                logText.text = inputText.text + "=";
                break;
            case Step.FirstValueInput:
                logText.text = inputText.text + "=";                
                break;
            case Step.OperationInput:
                logText.text = operationFormat(calculator.X) + calculator.X + "=";
                inputText.text = calculator.X;
                break;
            case Step.SecondValueInput:
                inputText.text = calculator.Result;
                logText.text = operationFormat(calculator.X) + calculator.Y + "=";
                break;
            case Step.ResultOperation:
                inputText.text = calculator.Result;
                logText.text = operationFormat(calculator.X) + calculator.Y + "="; 
                break;
            case Step.ZeroDivision:
                var x = calculator.X;
                Clear();
                if (x == "0")
                    inputText.text = "Результат не определен!";                
                else
                    inputText.text = "На ноль делить нельзя!";
                logText.text = operationFormat(x);
                break;

        }      
    }
}
