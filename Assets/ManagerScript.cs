using System;
using UnityEngine;
using UnityEngine.UI;
public interface ICalculator
{
    ICalculator ChangeState();
    void ActivateOperation();
    void OperationTypeSaving(Func<float, float, float?> operation);
    void ModifyInputValue(Func<float, float> modifier);
    void TextParse(string digit);
}
public interface IOutputFormatter
{
    void TextParse(Text inputText, Text logText);
    void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format);
    void ActivateOperation(Text inputText, Text logText);
    void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat);
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
        inputText.text = calculator.X;
        nextState = new FirstValueInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.X);        
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = inputText.text + "=";
    }
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
        inputText.text = modifierFormat(calculator.X);
        nextState = new FirstValueInputStateFormatter(format);
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
        inputText.text = calculator.X;        
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.X);
        inputText.text = calculator.X;
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = inputText.text + "=";
        nextState = new StartingStateFormatter(format);
    }
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
        inputText.text = modifierFormat(calculator.X);
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
        inputText.text = calculator.Y;
        nextState = new SecondValueInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.X);
        inputText.text = calculator.X;
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
            logText.text = format(calculator.X) + calculator.X + "=";
            inputText.text = calculator.Result;
            nextState = new ResultOperationStateFormatter(format);
        }
    }
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
        inputText.text = modifierFormat(calculator.Y);
        nextState = new SecondValueInputStateFormatter(format);
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
        inputText.text = calculator.Y;
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {        
        if (calculator.NextState is ZeroDivisionState)
        {
            logText.text = this.format(calculator.X) + format(calculator.Y);
            inputText.text = "На ноль делить нельзя";
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            logText.text = format(calculator.Result);
            inputText.text = default;
            nextState = new OperationInputStateFormatter(format);
        }
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = format(calculator.X) + calculator.Y + "=";
        if (calculator.NextState is ZeroDivisionState)
        {
            inputText.text = "На ноль делить нельзя";
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            inputText.text = calculator.Result;
            nextState = new ResultOperationStateFormatter(format);
        }
    }
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
        inputText.text = modifierFormat(calculator.Y);
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
        inputText.text = calculator.X;
        nextState = new ResultOperationInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.X);
        inputText.text = default;
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        inputText.text = calculator.Result;
        logText.text = format(calculator.X) + calculator.Y + "=";
    }
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
        logText.text = default;
        inputText.text = modifierFormat(calculator.Result);
        nextState = new ResultOperationInputStateFormatter(format);
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
        inputText.text = calculator.X;
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        logText.text = format(calculator.X);
        inputText.text = calculator.X;
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = format(calculator.X) + calculator.Y + "=";
        inputText.text = calculator.Result;
        nextState = new ResultOperationStateFormatter(format);
    }
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
        inputText.text = modifierFormat(calculator.X);
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
        inputText.text = calculator.X;
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
    public void ModifyInputValue(Text inputText, Text logText, Func<string, string> modifierFormat)
    {
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
        Debug.LogWarning(calculator.X + " " + calculator.Y + " " + calculator.Result);
    }    
    public void SendCheckableOperation(Func<float,float,float?> callback, Func<string, string> format) 
    {
        icalculator.OperationTypeSaving(callback);
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.OperationTypeSaving(inputText, logText, format);
        outputFormatter = outputFormatter.ChangeState();
        Debug.LogWarning(calculator.X + " " + calculator.Y + " " + calculator.Result);
    }
    public void Equal()
    {
        icalculator.ActivateOperation();
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.ActivateOperation(inputText, logText);
        outputFormatter = outputFormatter.ChangeState();
        Debug.LogWarning(calculator.X + " " + calculator.Y + " " + calculator.Result);
    }
    public void ModifyInput(Func<string, string> modifierFormat, Func<float, float> modifier)
    {
        icalculator.ModifyInputValue(modifier);
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.ModifyInputValue(inputText, logText, modifierFormat);
        outputFormatter = outputFormatter.ChangeState();
        Debug.LogWarning(calculator.X + " " + calculator.Y + " " + calculator.Result);
    }
}
