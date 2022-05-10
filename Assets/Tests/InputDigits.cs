using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;

public class InputDigits
{
    float? Plus(float x, float y)
    {
        return x + y;
    }

    [Test]
    public void Clear()
    {
        var calculator = new Calculator();
        calculator.Clear();
        Assert.IsTrue(calculator.X == "0");
    }
    [Test]
    public void TypeFirstDigitOfFirstValueInput()
    {
        var calculator = new Calculator(0, 0);
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("3");
        icalculator = icalculator.ChangeState();
        Assert.IsTrue(calculator.X == "3");
        Assert.IsTrue(icalculator is FirstValueInputState);
    }
    [Test]
    public void TypeSecondDigitOfFirstValueInput()
    {
        var calculator = new Calculator(2,0);
        ICalculator icalculator = new FirstValueInputState(calculator);
        icalculator.TextParse("3");
        icalculator = icalculator.ChangeState();
        Assert.IsTrue(calculator.X == "23");
        Assert.IsTrue(icalculator is FirstValueInputState);
    }
    [Test]
    public void TypeFirstDigitOfSecondValueInput() 
    {
        var calculator = new Calculator(2,0, Plus);
        ICalculator icalculator = new OperationInputState(calculator);
        icalculator.TextParse("2");
        icalculator = icalculator.ChangeState();
        Assert.IsTrue(calculator.Y == "2");
        Assert.IsTrue(icalculator is SecondValueInputState);
    }
    [Test]
    public void TypeSecondDigitOfSecondValueInput()
    {
        var calculator = new Calculator(2, 3, Plus);
        ICalculator icalculator = new SecondValueInputState(calculator);
        icalculator.TextParse("2");
        icalculator = icalculator.ChangeState();
        Assert.IsTrue(calculator.Y == "32");
        Assert.IsTrue(icalculator is SecondValueInputState);
    }
    [Test]
    public void TypeFirstDigitOfFirstValueAfterZeroDivisoin()
    {
        var calculator = new Calculator(212, 0);
        ICalculator icalculator = new ZeroDivisionState(calculator);
        icalculator.TextParse("2");
        icalculator = icalculator.ChangeState();
        Assert.IsTrue(calculator.X == "2");
        Assert.IsTrue(icalculator is FirstValueInputState);
    }
}
