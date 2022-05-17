using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classes that represent different states of calculator's ooperation process.
public class StartingState : ICalculator                     
{
    private Calculator calculator;
    private ICalculator nextState;
    public StartingState(Calculator calculator) 
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(digit);
        nextState = new FirstValueInputState(calculator);        
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        nextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.result = calculator.x;        
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        nextState = new ModifyFirstValueState(calculator);
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new StartingState(calculator);
        return nextState != null ? nextState : new StartingState(calculator);
    }

    
}
public class FirstValueInputState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public FirstValueInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {   
        calculator.x = float.Parse(calculator.X + digit);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        nextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.result = calculator.x;
        nextState = new StartingState(calculator);
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        nextState = new ModifyFirstValueState(calculator);
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new FirstValueInputState(calculator);
        return nextState != null ? nextState : new FirstValueInputState(calculator);
    }

    
}
public class ModifyFirstValueState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public ModifyFirstValueState (Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        if (digit == "0")
            calculator.TrailingZeroes += digit;
        else
        {
            calculator.x = float.Parse(calculator.X + digit);
            calculator.x = calculator.modifier(calculator.x);
            calculator.TrailingZeroes = string.Empty;
        }
        //nextState = new FirstValueInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        nextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.result = calculator.x;
        nextState = new StartingState(calculator);
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        calculator.x = calculator.modifier(calculator.x);
    }

    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new ModifyFirstValueState(calculator);
        return nextState != null ? nextState : new ModifyFirstValueState(calculator);
    }

}
public class OperationInputState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public OperationInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.y = float.Parse(digit);
        calculator.result = calculator.operation(calculator.x, calculator.y);
        nextState = new SecondValueInputState(calculator);
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
            nextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp.Value;
            nextState = new ResultOperationState(calculator);
        }
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        nextState = new ModifySecondValueState(calculator);
    }

    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new OperationInputState(calculator);
        return nextState != null ? nextState : new OperationInputState(calculator);
    }
}
public class SecondValueInputState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public SecondValueInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.y = float.Parse(calculator.Y + digit);
        calculator.result = calculator.operation(calculator.x, calculator.y);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        var tmp = calculator.operation(calculator.x, calculator.y);
        if (tmp == null)
        {
            nextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp;
            calculator.x = calculator.result.Value;
            nextState = new OperationInputState(calculator);
        }
        calculator.operation = operation;
    }
    public void ActivateOperation()
    {
        var tmp1 = calculator.operation(calculator.x, calculator.y);
        if (tmp1 == null)
        {
            nextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp1.Value;
            nextState = new ResultOperationState(calculator);
        }
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        nextState = new ModifySecondValueState(calculator);
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new SecondValueInputState(calculator);
        return nextState != null ? nextState : new SecondValueInputState(calculator);
    }    
}
public class ModifySecondValueState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public ModifySecondValueState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.y = float.Parse(calculator.Y + digit);        
        calculator.y = calculator.modifier(calculator.y);
        calculator.result = calculator.operation(calculator.x, calculator.y);
        nextState = new SecondValueInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        var tmp = calculator.operation(calculator.x, calculator.y);
        if (tmp == null)
        {
            nextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp;
            calculator.x = calculator.result.Value;
            nextState = new OperationInputState(calculator);
        }
        calculator.operation = operation;
    }
    public void ActivateOperation()
    {
        var tmp1 = calculator.operation(calculator.x, calculator.y);
        if (tmp1 == null)
        {
            nextState = new ZeroDivisionState(calculator);
        }
        else
        {
            calculator.result = tmp1.Value;
            nextState = new ResultOperationState(calculator);
        }
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        calculator.y = calculator.modifier(calculator.y);
    }

    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new ModifySecondValueState(calculator);
        return nextState != null ? nextState : new ModifySecondValueState(calculator);
    }

}
public class ResultOperationState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public ResultOperationState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(digit);
        nextState = new ResultOperationInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        calculator.x = calculator.result.Value;
        calculator.y = 0;
        nextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.x = calculator.result.Value;
        calculator.result = calculator.operation(calculator.x, calculator.y);
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        nextState = new ModifyResultValueState(calculator);
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new ResultOperationState(calculator);
        return nextState != null ? nextState : new ResultOperationState(calculator);
    }    
}
public class ResultOperationInputState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public ResultOperationInputState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(calculator.X + digit);
        calculator.result = calculator.x;
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        nextState = new OperationInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.result = calculator.operation(calculator.x, calculator.y).Value;
        nextState = new ResultOperationState(calculator);
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        nextState = new ModifyResultValueState(calculator);
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new ResultOperationInputState(calculator);
        return nextState != null ? nextState : new ResultOperationInputState(calculator);
    }    
}
public class ModifyResultValueState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public ModifyResultValueState(Calculator calculator)
    {
        this.calculator = calculator;
    }

    public void TextParse(string digit)
    {
        calculator.x = calculator.result.Value;
        calculator.x = float.Parse(calculator.X + digit);
        calculator.x = calculator.modifier(calculator.x);        
        nextState = new ResultOperationInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
        calculator.operation = operation;
        nextState = new SecondValueInputState(calculator);
    }
    public void ActivateOperation()
    {
        calculator.x = calculator.result.Value;
        calculator.result = calculator.operation(calculator.x, calculator.y).Value;
        nextState = new ResultOperationState(calculator);
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
        calculator.modifier = modifier;
        calculator.x = calculator.modifier(calculator.x);
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new ModifyResultValueState(calculator);
        return nextState != null ? nextState : new ModifyResultValueState(calculator);
    }    
}
public class ZeroDivisionState : ICalculator
{
    private Calculator calculator;
    private ICalculator nextState;
    public ZeroDivisionState(Calculator calculator)
    {
        this.calculator = calculator;
    }
    public void TextParse(string digit)
    {
        calculator.x = float.Parse(digit);
        nextState = new FirstValueInputState(calculator);
    }
    public void OperationTypeSaving(Func<float, float, float?> operation)
    {
    }
    public void ActivateOperation()
    {
        calculator.Clear();
        nextState = new StartingState(calculator);
    }
    public void ModifyInputValue(Func<float, float> modifier)
    {
    }
    public ICalculator ChangeState()
    {
        calculator.NextState = nextState != null ? nextState : new ZeroDivisionState(calculator);
        return nextState != null ? nextState : new ZeroDivisionState(calculator);
    }
}


//                                         Old code starts here.
public class Calculator
{ // MADE ALL VARIABLES AND FIELDS PUBLIC SO IT'S POSSIBLE TO REACH THEM OUTSIDE THE CLASS
    public float x;
    public float y;
    public float? result;
    public Func<float, float, float?> operation;
    public Func<float, float> modifier;
    public string TrailingZeroes;
    public string X => x.ToString() + TrailingZeroes;    
    public string Y => y.ToString() + TrailingZeroes;
    public string Result => result?.ToString();
    public ICalculator NextState;
    public Calculator()
    {
        Clear();
    }
    public Calculator(float x, float y, Func<float, float, float?> operation = null)
    {
        this.x = x;
        this.y = y;
        this.operation = operation;
    }
    public void Clear()
    {
        x = 0;
        y = default;
        result = default;
        NextState = new StartingState(this);
    }
}