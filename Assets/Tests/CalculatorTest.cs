using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.TestTools;


public class CalculatorTest
{
    [Test]
    public void Clear()
    {
        var calculator = new Calculator();
        calculator.Clear();
        Assert.IsTrue(calculator.X == 0);
    }

   
}
