using System;
using UnityEngine;
using UnityEngine.UI;
public interface ICalculator
{
    ICalculator ChangeState();
    void ActivateOperation();
    void OperationTypeSaving(Func<float, float, float?> operation);
    void TextParse(string digit);
}
public interface IOutputFormatter
{
    void TextParse(Text inputText, Text logText);
    void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format);
    void ActivateOperation(Text inputText, Text logText);
    void GetCalculatorValues(Calculator calculator);
    IOutputFormatter ChangeState();
}
public class StartingStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public StartingStateFormatter(Func<string, string> format)
    {
        this.format = format;        
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        inputText.text = calculator.x.ToString();
        nextState = new FirstValueInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.x.ToString());        
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = inputText.text + "=";
    }

    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new StartingStateFormatter(format);
    }
}
public class FirstValueInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public FirstValueInputStateFormatter(Func<string, string> format)
    {
        this.format = format;        
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        inputText.text = calculator.x.ToString();
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.x.ToString());
        inputText.text = calculator.x.ToString();
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = inputText.text + "=";
        nextState = new StartingStateFormatter(format);
    }
    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new FirstValueInputStateFormatter(format);
    }
}
public class OperationInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public OperationInputStateFormatter(Func<string, string> format)
    {
        this.format = format;
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        inputText.text = calculator.y.ToString();
        nextState = new SecondValueInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.x.ToString());
        inputText.text = calculator.x.ToString();
        this.format = format;
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        if (calculator.NextState is ZeroDivisionState)
        {
            inputText.text = "Результат не отпределен!";
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            logText.text = format(calculator.x.ToString()) + calculator.x.ToString() + "=";
            inputText.text = calculator.result.ToString();
            nextState = new ResultOperationStateFormatter(format);
        }
    }
    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new OperationInputStateFormatter(format);
    }
}
public class SecondValueInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public SecondValueInputStateFormatter(Func<string, string> format)
    {
        this.format = format;        
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        inputText.text = calculator.y.ToString();
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {        
        if (calculator.NextState is ZeroDivisionState)
        {
            logText.text = this.format(calculator.x.ToString()) + format(calculator.y.ToString());
            inputText.text = "На ноль делить нельзя";
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            logText.text = format(calculator.result.ToString());
            inputText.text = default;
            nextState = new OperationInputStateFormatter(format);
        }
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = format(calculator.x.ToString()) + calculator.y.ToString() + "=";
        if (calculator.NextState is ZeroDivisionState)
        {
            inputText.text = "На ноль делить нельзя";
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            inputText.text = calculator.result.ToString();
            nextState = new ResultOperationStateFormatter(format);
        }
    }
    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new SecondValueInputStateFormatter(format);
    }
}
public class ResultOperationStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public ResultOperationStateFormatter(Func<string, string> format)
    {        
        this.format = format;
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        logText.text = default;
        inputText.text = calculator.x.ToString();
        nextState = new ResultOperationInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.x.ToString());
        inputText.text = default;
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        inputText.text = calculator.result.ToString();
        logText.text = format(calculator.x.ToString()) + calculator.y.ToString() + "=";
    }
    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new ResultOperationStateFormatter(format);
    }
}
public class ResultOperationInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public ResultOperationInputStateFormatter(Func<string, string> format)
    {
        this.format = format;
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        inputText.text = calculator.x.ToString();
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.x.ToString());
        inputText.text = calculator.x.ToString();
        nextState = new SecondValueInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = format(calculator.x.ToString()) + calculator.y.ToString() + "=";
        inputText.text = calculator.result.ToString();
        nextState = new ResultOperationStateFormatter(format);
    }
    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new ResultOperationInputStateFormatter(format);
    }
}
public class ZeroDivisionStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    private Func<string, string> format;
    public ZeroDivisionStateFormatter(Func<string, string> format)
    {
        this.format = format;        
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {
        inputText.text = calculator.x.ToString();
        logText.text = default;
        nextState = new FirstValueInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {        
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        inputText.text = "0";
        logText.text = default;
        nextState = new StartingStateFormatter(format);
    }
    public IOutputFormatter ChangeState()
    {
        return nextState != null ? nextState : new ZeroDivisionStateFormatter(format);
    }
}

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private Text inputText;
    [SerializeField] private Text logText;
    Calculator calculator;
    IOutputFormatter outputFormatter;
    ICalculator icalculator;

    private void Start()
    {
        calculator = new Calculator();
        icalculator = new StartingState(calculator);
        outputFormatter = new StartingStateFormatter(null);
        Clear();
    }
    private void Update()
    {
        Debug.LogWarning(icalculator.GetType());
        Debug.LogWarning(outputFormatter.GetType());
    }
    public void Clear() 
    {
        calculator.Clear();
        inputText.text = "0";
        logText.text = default;
        icalculator = new StartingState(calculator);
        outputFormatter = new StartingStateFormatter(null);
    }
    public void Type(string buttonName)
    {
        icalculator.TextParse(buttonName);
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.TextParse(inputText, logText);
        outputFormatter = outputFormatter.ChangeState();
    }    
    public void SendCheckableOperation(Func<float,float,float?> callback, Func<string, string> format) 
    {
        icalculator.OperationTypeSaving(callback);
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.OperationTypeSaving(inputText, logText, format);
        outputFormatter = outputFormatter.ChangeState();
    }
    public void Equal()
    {
        icalculator.ActivateOperation();
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.ActivateOperation(inputText, logText);
        outputFormatter = outputFormatter.ChangeState();
    }
}
