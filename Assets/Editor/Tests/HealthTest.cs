using System.Collections;
using System.Collections.Generic;
using HealthSystem;
using NUnit.Framework;
using Obvious.Soap;
using UnityEngine;

public class HealthTest
{
    public IndividualHealth health = null;

    [SetUp]
    public void SetUp()
    {
        health = new IndividualHealth(ScriptableObject.CreateInstance<IntVariable>(), ScriptableObject.CreateInstance<IntVariable>());
        health.maxHealthPoint.Value = 100;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(health.healthPoint);
        Object.DestroyImmediate(health.maxHealthPoint);
    }
    
    [Test]
    public void _10_Damage_Receive_To_100_HealthPoint()
    {
        health.healthPoint.Value = 100;
        // health.healthPoint.OnValueChanged += value => hp = value;
        health.TakeDamage(10);
        Assert.AreEqual(90, health.healthPoint.Value);
    }

    [Test]
    public void _10_Damage_Receive_To_0_Health_Point()
    {
        health.healthPoint.Value = 0;
        // health.healthPoint.OnValueChanged += value => hp = value;
        health.TakeDamage(10);
        Assert.AreEqual(0, health.healthPoint.Value);
    }

    [Test]
    public void _Heal_10_From_10_Health_Point()
    {
        health.healthPoint.Value = 10;
        health.RecoverHealthPoint(20);
        Assert.AreEqual(30, health.healthPoint.Value);
    }
    [Test]
    public void _Heal_30_From_0_Health_Point()
    {
        health.healthPoint.Value = 0;
        health.RecoverHealthPoint(40);
        Assert.AreEqual(0, health.healthPoint.Value);
    }
    [Test]
    public void _Heal_30_From_100_Health_Point_And_100_Max_Health()
    {
        health.healthPoint.Value = 100;
        health.maxHealthPoint.Value = 100;
        health.RecoverHealthPoint(40);
        Assert.AreEqual(100, health.healthPoint.Value);
    }
    [Test]
    public void _Add_Max_Health_100()
    {
        health.maxHealthPoint.Value = 100;
        health.IncrementMaxHealthPoint(100);
        Assert.AreEqual(200, health.maxHealthPoint.Value);
    }
    [Test]
    public void _Add_Max_Health_Negative_100()
    {
        health.maxHealthPoint.Value = 100;
        health.IncrementMaxHealthPoint(-100);
        Assert.AreEqual(100, health.maxHealthPoint.Value);
    }
    
}


