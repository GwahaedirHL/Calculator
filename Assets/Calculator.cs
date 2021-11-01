using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
{
    private float x;
    private float y;
    private float? result;
    private float? cache;
    private Step step;
    private Func<float, float, float?> operation;
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
    }

    public void Clear()
    {
        x = 0;
        y = default;
        result = default;
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