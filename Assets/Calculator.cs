using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Interfaces of the three main elements of the calculator.

public interface IType
{
    void TextParse(string digit);
}

public interface ISendCheckableOperation
{
    void OperationTypeSaving(Func<float, float, float?> operation);
}

public interface IEqual
{
    void ActivateOperation();
}

// Classes that represent different states of calculator's ooperation process.

 // Step.Opening
public class StartingState : ICalculator                     
{
    private Calculator calculator; 
    public StartingState(Calculator calculator) 
    {
        this.calculator = calculator;       
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(digit);
        calculator.NextState = new FirstValueInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        calculator.NextState = new OperationInputState(calculator);

    }
    public void ActivateOperation()
    {
        calculator.result = calculator.x;     
    }

    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }
}

//Step.FirstvalueInput
public class FirstValueInputState : ICalculator
{
    private Calculator calculator;
    public FirstValueInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(calculator.x.ToString() + digit);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        calculator.NextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.result = calculator.x;
        calculator.NextState = new StartingState(calculator);
    }
    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }   
}

// Step.OperationInput
public class OperationInputState : ICalculator
{
    private Calculator calculator;

    public OperationInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }

    public void TextParse(string digit)
    {
        calculator.y = float.Parse(digit);
        calculator.NextState = new SecondValueInputState(calculator);

    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
    }
    public void ActivateOperation()
    {
        calculator.y = calculator.x;
        var tmp = calculator.operation(calculator.x, calculator.y);
        if (tmp == null)
        {
            calculator.NextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp.Value;
            calculator.NextState = new ResultOperationState(calculator);
        }
    }

    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }
}

//Step.SecondValueInput
public class SecondValueInputState : ICalculator
{
    private Calculator calculator;
    public SecondValueInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.y = float.Parse(calculator.y.ToString() + digit);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        var tmp = operation(calculator.x, calculator.y);
        if (tmp == null)
        {
            calculator.NextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp;
            calculator.x = calculator.result.Value;
            calculator.NextState = new OperationInputState(calculator);
        }
        calculator.operation = operation;
    }
    public void ActivateOperation()
    {
        var tmp1 = calculator.operation(calculator.x, calculator.y);
        if (tmp1 == null)
        {
            calculator.NextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp1.Value;
            calculator.NextState = new ResultOperationState(calculator);
        }
    }

    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }

    

    
}

//Step.ResultOperation
public class ResultOperationState : ICalculator
{
    private Calculator calculator;
    public ResultOperationState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(digit);
        calculator.result = calculator.x;
        calculator.NextState = new ResultOperationInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        calculator.x = calculator.result.Value;
        calculator.NextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
    }

    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }    
}

//Step.ResultOperationInput
public class ResultOperationInputState : ICalculator
{
    private Calculator calculator;
    public ResultOperationInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(calculator.x.ToString() + digit);
        calculator.result = calculator.x;
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
    }
    public void ActivateOperation()
    {
        calculator.x = calculator.result.Value;
        calculator.result = calculator.operation(calculator.x, calculator.y).Value;
    }
    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }    
}

//Step.ZeroDivision
public class ZeroDivisionState : ICalculator
{
    private Calculator calculator;
    public ZeroDivisionState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(digit);
        calculator.NextState = new FirstValueInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
    }
    public void ActivateOperation()
    {
    }
    public ICalculator ChangeState()
    {
        return calculator.NextState;
    }
}


//                                         Old code starts here.


public enum Step
{
    Opening,
    ZeroDivision,
    FirstValueInput,
    OperationInput,
    SecondValueInput,
    ResultOperation,
    ResultOperationInput,
}
public class Calculator
{ // MADE ALL VARIABLES AND FIELDS PUBLIC SO IT'S POSSIBLE TO REACH THEM OUTSIDE THE CLASS
    public float x;
    public float y;
    public float? result;
    public float? cache;
    private Step step;
    public Func<float, float, float?> operation;
    public ICalculator NextState;
    public Step Step => step;
    public string X => x.ToString();
    public string Y => y.ToString();
    public string Result => result?.ToString();
    public Calculator()
    {
        Clear();
    }
    public Calculator(float x, float y, Step step, Func<float, float, float?> operation = null)
    {
        this.x = x;
        this.y = y;
        this.step = step;
        this.operation = operation;
        NextState = new StartingState(this);
    }

    public void Clear()
    {
        x = 0;
        y = default;
        result = default;
        step = Step.Opening;
    }

    public void NewType(string digit)
    {
        
    }
    public void NewEqual()
    {

    }
    public void NewGetOperation(Func<float, float, float?> operation)
    {

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
                y = float.Parse(y.ToString() + digit);
                break;
            case Step.OperationInput:
                y = float.Parse(digit);
                step = Step.SecondValueInput;
                break;
            case Step.ZeroDivision:
                x = float.Parse(digit);
                step = Step.FirstValueInput;
                break;
            case Step.ResultOperation:
                x = float.Parse(digit);
                result = x;
                step = Step.ResultOperationInput;
                break;
            case Step.ResultOperationInput:
                x = float.Parse(x.ToString() + digit);
                result = x;
                break;
        }

    }
    public void SendCheckableOperation(Func<float, float, float?> callback)
    {
        switch (step)
        {
            case Step.Opening:
                operation = callback;
                step = Step.OperationInput;
                break;
            case Step.FirstValueInput:
                operation = callback;
                step = Step.OperationInput;
                break;
            case Step.OperationInput:
                operation = callback;
                break;
            case Step.SecondValueInput:
                var tmp = operation(x, y);
                if (tmp == null)
                {
                    step = Step.ZeroDivision;
                }
                else
                {
                    result = tmp;
                    x = result.Value;
                    step = Step.OperationInput;
                }
                operation = callback;
                break;
            case Step.ZeroDivision:
                break;
            case Step.ResultOperation:
                operation = callback;
                x = result.Value;
                step = Step.OperationInput;
                break;
        }
    }
    public void Equal()
    {
        switch (step)
        {
            case Step.Opening:
                result = x;
                break;
            case Step.FirstValueInput:
                result = x;
                step = Step.Opening;
                break;
            case Step.OperationInput:
                y = x;
                var tmp = operation(x, y);
                if (tmp == null)
                {
                    step = Step.ZeroDivision;
                }
                else
                {
                    result = tmp.Value;
                    step = Step.ResultOperation;
                }
                break;
            case Step.SecondValueInput:
                var tmp1 = operation(x, y);
                if (tmp1 == null)
                {
                    step = Step.ZeroDivision;
                }
                else
                {
                    result = tmp1.Value;
                    step = Step.ResultOperation;
                }
                break;
            case Step.ResultOperation:
            case Step.ResultOperationInput:
                x = result.Value;
                result = operation(x, y).Value;
                break;
        }
    }
}