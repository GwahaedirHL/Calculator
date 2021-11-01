using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;


public class InputDigits
{
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
        var calculator = new Calculator(0, 0, Step.Opening);
        calculator.Type("3");
        Assert.IsTrue(calculator.X == "3");
        Assert.IsTrue(calculator.Step == Step.FirstValueInput);
    }
    [Test]
    public void TypeSecondDigitOfFirstValueInput()
    {
        var calculator = new Calculator(2,0,Step.FirstValueInput);
        calculator.Type("3");
        Assert.IsTrue(calculator.X == "23");
        Assert.IsTrue(calculator.Step == Step.FirstValueInput);
    }
    [Test]
    public void TypeFirstDigitOfSecondValueInput() 
    {
        var calculator = new Calculator(2,0,Step.OperationInput);
        calculator.Type("2");
        Assert.IsTrue(calculator.Y == "2");
        Assert.IsTrue(calculator.Step == Step.SecondValueInput);
    }
    [Test]
    public void TypeSecondDigitOfSecondValueInput()
    {
        var calculator = new Calculator(2, 3, Step.SecondValueInput);
        calculator.Type("2");
        Assert.IsTrue(calculator.Y == "32");
        Assert.IsTrue(calculator.Step == Step.SecondValueInput);
    }
    [Test]
    public void TypeFirstDigitOfFirstValueAfterZeroDivisoin()
    {
        var calculator = new Calculator(212, 0, Step.ZeroDivision);
        calculator.Type("2");
        Assert.IsTrue(calculator.X == "2");
        Assert.IsTrue(calculator.Step == Step.FirstValueInput);
    }
}
