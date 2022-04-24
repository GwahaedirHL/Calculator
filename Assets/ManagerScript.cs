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

// Step.Opening
public class StartingStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public StartingStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
        this.format = format;
    }

    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {        
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
        return nextState;
    }
}

//Step.FirstvalueInput
public class FirstValueInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public FirstValueInputStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
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
        inputText.text = default;
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        logText.text = inputText.text + "=";
        nextState = new StartingStateFormatter(format);
    }
    public IOutputFormatter ChangeState()
    {
        return nextState;
    }
}

// Step.OperationInput
public class OperationInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public OperationInputStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
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

        logText.text = format(calculator.x.ToString()) + calculator.x.ToString() + "=";
        inputText.text = calculator.x.ToString();
        calculator.y = calculator.x;
        var tmp = calculator.operation(calculator.x, calculator.y);
        if (tmp == null)
        {
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            calculator.result = tmp.Value;
            nextState = new ResultOperationStateFormatter(format);
        }
    }

    public IOutputFormatter ChangeState()
    {
        return nextState;
    }
}

//Step.SecondValueInput
public class SecondValueInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public SecondValueInputStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
        this.format = format;
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }

    public void TextParse(Text inputText, Text logText)
    {
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
        
        if (calculator.NextState is ZeroDivisionState)
        {
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            nextState = new OperationInputStateFormatter(format);
        }
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        inputText.text = calculator.result.ToString();
        logText.text = format(calculator.x.ToString()) + calculator.y.ToString() + "=";
        if (calculator.NextState is ZeroDivisionState)
        {
            nextState = new ZeroDivisionStateFormatter(format);
        }
        else
        {
            nextState = new ResultOperationStateFormatter(format);
        }
    }

    public IOutputFormatter ChangeState()
    {
        return nextState;
    }
}

//Step.ResultOperation
public class ResultOperationStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public ResultOperationStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
        this.format = format;
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {        
        nextState = new ResultOperationInputStateFormatter(format);
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {        
        nextState = new OperationInputStateFormatter(format);
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        inputText.text = calculator.result.ToString();
        logText.text = format(calculator.x.ToString()) + calculator.y.ToString() + "=";
    }

    public IOutputFormatter ChangeState()
    {
        return nextState;
    }
}

//Step.ResultOperationInput
public class ResultOperationInputStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public ResultOperationInputStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
        this.format = format;
    }
    public void GetCalculatorValues(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(Text inputText, Text logText)
    {        
    }
    public void OperationTypeSaving(Text inputText, Text logText, Func<string, string> format)
    {
    }
    public void ActivateOperation(Text inputText, Text logText)
    {        
    }
    public IOutputFormatter ChangeState()
    {
        return nextState;
    }
} // This class do nothing, is it worth keeping?

//Step.ZeroDivision
public class ZeroDivisionStateFormatter : IOutputFormatter
{
    private Calculator calculator;
    private IOutputFormatter nextState;
    //private string previoslyPressedButtonName;
    private Func<string, string> format;
    public ZeroDivisionStateFormatter(Func<string, string> format)
    {
        //this.previoslyPressedButtonName = previoslyPressedButtonName;
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
        logText.text = this.format(calculator.x.ToString()) + format(calculator.y.ToString());
        inputText.text = "На ноль делить нельзя!";
    }
    public void ActivateOperation(Text inputText, Text logText)
    {
        var x = calculator.X;
        calculator.Clear();
        if (x == "0")
            inputText.text = "Результат не определен!";
        else
            inputText.text = "На ноль делить нельзя!";
        logText.text = format(x);
    }
    public IOutputFormatter ChangeState()
    {
        return nextState;
    }
}

public class ManagerScript : MonoBehaviour
{
    [SerializeField] private Text inputText;
    [SerializeField] private Text logText;
    Calculator calculator;
    IOutputFormatter outputFormatter;
    private Func<string, string> operationFormat;          //unused
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
       // Debug.LogWarning(calculator.Step.ToString());
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
        icalculator = icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.TextParse(inputText, logText);
        outputFormatter = outputFormatter.ChangeState();

        //calculator.Type(buttonName);
        //switch (calculator.Step)
        //{
        //    case Step.Opening:
        //    case Step.FirstValueInput:
        //        inputText.text = calculator.X;
        //        break;
        //    case Step.SecondValueInput:
        //    case Step.OperationInput:
        //        inputText.text = calculator.Y;
        //        break;
        //    case Step.ZeroDivision:
        //        inputText.text = calculator.X;
        //        logText.text = default;
        //        break;
        //    case Step.ResultOperation:                
        //        break;
        //}
    }
    
    public void SendCheckableOperation(Func<float,float,float?> callback, Func<string, string> format) 
    {
        icalculator.OperationTypeSaving(callback);
        icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.OperationTypeSaving(inputText, logText, format);
        outputFormatter = outputFormatter.ChangeState();

        //calculator.SendCheckableOperation(callback);
        //switch (calculator.Step)
        //{
        //    case Step.Opening:
        //        logText.text = format(calculator.X);
        //        operationFormat = format;
        //        break;
        //    case Step.FirstValueInput:
        //        logText.text = format(calculator.X);
        //        operationFormat = format;
        //        inputText.text = default;                
        //        break;
        //    case Step.OperationInput:
        //        logText.text = format(calculator.X);
        //        inputText.text = calculator.X.ToString();
        //        operationFormat = format;
        //        break;  
        //    case Step.ZeroDivision:
        //        logText.text = operationFormat(calculator.X) + format(calculator.Y);
        //        inputText.text = "На ноль делить нельзя!";
        //        break;
        //}


    }
    public void Equal()
    {
        icalculator.ActivateOperation();
        icalculator.ChangeState();
        outputFormatter.GetCalculatorValues(calculator);
        outputFormatter.ActivateOperation(inputText, logText);
        outputFormatter = outputFormatter.ChangeState();

        //calculator.Equal();
        //switch (calculator.Step)
        //{
        //    case Step.Opening:
        //        logText.text = inputText.text + "=";
        //        break;
        //    case Step.FirstValueInput:
        //        logText.text = inputText.text + "=";                
        //        break;
        //    case Step.OperationInput:
        //        logText.text = operationFormat(calculator.X) + calculator.X + "=";
        //        inputText.text = calculator.X;
        //        break;
        //    case Step.SecondValueInput:
        //        inputText.text = calculator.Result;
        //        logText.text = operationFormat(calculator.X) + calculator.Y + "=";
        //        break;
        //    case Step.ResultOperation:
        //        inputText.text = calculator.Result;
        //        logText.text = operationFormat(calculator.X) + calculator.Y + "="; 
        //        break;
        //    case Step.ZeroDivision:
        //        var x = calculator.X;
        //        Clear();
        //        if (x == "0")
        //            inputText.text = "Результат не определен!";                
        //        else
        //            inputText.text = "На ноль делить нельзя!";
        //        logText.text = operationFormat(x);
        //        break;
        //}      
    }
}
