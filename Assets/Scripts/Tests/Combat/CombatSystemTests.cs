using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CombatSystemTests
{
    [TestCase(2, 0, 2)] 
    [TestCase(2, 2, 0)] 
    [TestCase(0, 0, 0)] 
    [TestCase(2, 10, 0)]
    public void DamageCalcTest(int attack, int defense, int expectedDamage)
    {
        int result = CombatSystem.CalculateDamage(attack, defense);
        Assert.AreEqual(expectedDamage, result);
    }
}
