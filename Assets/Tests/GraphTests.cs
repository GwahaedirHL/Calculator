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
        calculator.Type("1");
        calculator.Type("1");
        calculator.Type("1");
        Assert.IsTrue(calculator.X == "111");
    }
    [Test]
    public void OneOnePlusEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "11");
        Assert.IsTrue(calculator.Y == "11");
        Assert.IsTrue(calculator.Result == "22");
    }
    [Test]
    public void OneOneEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "11");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "11");
    }
    [Test]
    public void OnePlusOneEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OnePlusPlusEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OnePlusEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OneEqualOne()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.Equal();
        calculator.Type("1");
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void OneEqualPlusEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.Equal();
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void OneEqualEqual()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.Equal();
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusOneOneEqual()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.Type("1");
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "11");
        Assert.IsTrue(calculator.Result == "11");
    }
    [Test]
    public void PlusOnePlusEqual()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void PlusOneEqual()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusPlusOneEqual()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.SendCheckableOperation(Plus);
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusPlusPlusEqual()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.SendCheckableOperation(Plus);
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void PlusEqualOne()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        calculator.Type("1");
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void PlusEqualPlus()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        calculator.SendCheckableOperation(Plus);
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void PlusEqualEqual()
    {
        var calculator = new Calculator();
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EqualOneOne()
    {
        var calculator = new Calculator();
        calculator.Equal();
        calculator.Type("1");
        calculator.Type("1");
        Assert.IsTrue(calculator.X == "11");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EqualOnePlusEqual()
    {
        var calculator = new Calculator();
        calculator.Equal();
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "2");
    }
    [Test]
    public void EqualOneEqual()
    {
        var calculator = new Calculator();
        calculator.Equal();
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void EqualPlusOneEqual()
    {
        var calculator = new Calculator();
        calculator.Equal();
        calculator.SendCheckableOperation(Plus);
        calculator.Type("1");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void EqualPlusPlusEqual()
    {
        var calculator = new Calculator();
        calculator.Equal();
        calculator.SendCheckableOperation(Plus);
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        Assert.IsTrue(calculator.X == "0");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EqualEqualOne()
    {
        var calculator = new Calculator();
        calculator.Equal();
        calculator.Equal();
        calculator.Type("1");
        Assert.IsTrue(calculator.X == "1");
        Assert.IsTrue(calculator.Y == "0");
        Assert.IsTrue(calculator.Result == "0");
    }
    [Test]
    public void EquasionExample()
    {
        var calculator = new Calculator();
        calculator.Type("1");
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        calculator.Equal();
        Assert.IsTrue(calculator.X == "2");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "3");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "3");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "4");
        calculator.Type("6");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "6");
        Assert.IsTrue(calculator.Y == "1");
        Assert.IsTrue(calculator.Result == "7");

    }
    [Test]
    public void OneMoreEquasionExample()
    {
        var calculator = new Calculator();
        calculator.Type("65");
        calculator.SendCheckableOperation(Plus);
        calculator.Equal();
        calculator.Equal();
        Assert.AreEqual("130", calculator.X);
        Assert.AreEqual("65", calculator.Y);
        Assert.AreEqual("195", calculator.Result);
        calculator.SendCheckableOperation(Minus);
        calculator.Type("5");
        calculator.SendCheckableOperation(Divide);
        Assert.IsTrue(calculator.X == "190");
        Assert.IsTrue(calculator.Y == "5");
        Assert.IsTrue(calculator.Result == "190");
        calculator.Equal();
        Assert.IsTrue(calculator.X == "190");
        Assert.IsTrue(calculator.Y == "190");
        Assert.IsTrue(calculator.Result == "1");
    }
    [Test]
    public void ZeroDivision() 
    {
        var calculator = new Calculator();
        calculator.Type("52");
        calculator.SendCheckableOperation(Divide);
        calculator.Type("0");
        calculator.Equal();
        Assert.AreEqual("52", calculator.X);
        Assert.AreEqual("0", calculator.Y);
        Assert.IsNull(calculator.Result);    
    }
    [Test]
    public void SixMultiplyThreeEqualEqual()
    {
        var calculator = new Calculator();
        calculator.Type("6");
        calculator.SendCheckableOperation(Multiply);
        calculator.Type("3");
        calculator.Equal();
        calculator.Equal();
        Assert.AreEqual("18", calculator.X);
        Assert.AreEqual("3", calculator.Y);
        Assert.AreEqual("54", calculator.Result);
    }
}
