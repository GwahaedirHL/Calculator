using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class GraphTests
{
    float? Plus(float x, float y)
    {
        return x + y;
    }
    float? Minus(float x, float y)
    {
        return x - y;
    }
    float? Multiply(float x, float y)
    {
        return x * y;
    }
    float? Divide(float x, float y)
    {
        if (y == 0)
            return null;
        return x / y;
    }
    [Test]
    public void OneOneOne()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        
        icalculator.TextParse("1");
        icalculator.TextParse("1");
        icalculator.TextParse("1");
        Assert.IsTrue(calculator.X == "111");
    }
    [Test]
    public void OneOnePlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "11");
        Assert.IsTrue(calculator.Y == "11");
        Assert.IsTrue(calculator.Result == "22");
    }
    [Test]
    public void OneOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "11");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "11");
    }
    [Test]
    public void OnePlusOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OnePlusPlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OnePlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OneEqualOne()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        icalculator.TextParse("1");
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void OneEqualPlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OneEqualEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusOneOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.TextParse("1");
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "11");
        Assert.IsTrue(calculator.Result == "11");
    }
    [Test]
    public void PlusOnePlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void PlusOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusPlusOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.OperationTypeSaving(Plus);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusPlusPlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.OperationTypeSaving(Plus);
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void PlusEqualOne()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        icalculator.TextParse("1");
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusEqualPlus()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        icalculator.OperationTypeSaving(Plus);
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void PlusEqualEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EqualOneOne()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.ActivateOperation();
        icalculator.TextParse("1");
        icalculator.TextParse("1");
        Assert.IsTrue(calculator.X == "11");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EqualOnePlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.ActivateOperation();
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void EqualOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.ActivateOperation();
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void EqualPlusOneEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.ActivateOperation();
        icalculator.OperationTypeSaving(Plus);
        icalculator.TextParse("1");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void EqualPlusPlusEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.ActivateOperation();
        icalculator.OperationTypeSaving(Plus);
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EqualEqualOne()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.ActivateOperation();
        icalculator.ActivateOperation();
        icalculator.TextParse("1");
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EquasionExample()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("1");
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "2");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "3");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "3");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "4");
        icalculator.TextParse("6");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "6");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "7");

    }
    [Test]
    public void OneMoreEquasionExample()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("65");
        icalculator.OperationTypeSaving(Plus);
        icalculator.ActivateOperation();
        icalculator.ActivateOperation();
        Assert.AreEqual("130", calculator.X);
        Assert.AreEqual("65", calculator.Y);
        Assert.AreEqual("195", calculator.Result);
        icalculator.OperationTypeSaving(Minus);
        icalculator.TextParse("5");
        icalculator.OperationTypeSaving(Divide);
        Assert.IsTrue(calculator.X == "190");
        Assert.IsTrue(calculator.Y == "5");
        Assert.IsTrue(calculator.Result == "190");
        icalculator.ActivateOperation();
        Assert.IsTrue(calculator.X == "190");
        Assert.IsTrue(calculator.Y == "190");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void ZeroDivision() 
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("52");
        icalculator.OperationTypeSaving(Divide);
        icalculator.TextParse("0");
        icalculator.ActivateOperation();
        Assert.AreEqual("52", calculator.X);
        Assert.AreEqual("0", calculator.Y);
        Assert.IsNull(calculator.Result);    
    }
    [Test]
    public void SixMultiplyThreeEqualEqual()
    {
        var calculator = new Calculator();
        ICalculator icalculator = new StartingState(calculator);
        icalculator.TextParse("6");
        icalculator.OperationTypeSaving(Multiply);
        icalculator.TextParse("3");
        icalculator.ActivateOperation();
        icalculator.ActivateOperation();
        Assert.AreEqual("18", calculator.X);
        Assert.AreEqual("3", calculator.Y);
        Assert.AreEqual("54", calculator.Result);
    }
}
