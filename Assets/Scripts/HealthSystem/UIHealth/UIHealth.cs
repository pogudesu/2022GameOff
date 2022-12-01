using System;
using HealthSystem;
using Obvious.Soap;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField]private GameObject healthForeground;
    private float maxWidth;
    private RectTransform healthRect;
    [SerializeField] private IntVariable health;
    [SerializeField] private IntVariable maxhealth;
    private void Awake()
    {
        healthRect = healthForeground.GetComponent<RectTransform>();
        maxWidth = healthRect.sizeDelta.x;
        OnChangedHealthPointValue(health.Value);
    }

    private void OnEnable()
    {
        health.OnValueChanged += OnChangedHealthPointValue;
    }

    private void OnDisable()
    {
        health.OnValueChanged -= OnChangedHealthPointValue;

    }

    private void OnChangedHealthPointValue(int value)
    {
        float percentage = (float)value / (float)maxhealth.Value;
        // float percentage = Mathf.Lerp(0, maxhealth.Value, value);
        float currentValueWidth = maxWidth * percentage;
        // float currentValueWidth = Mathf.InverseLerp(0, maxWidth, percentage);
        healthRect.sizeDelta = new Vector2(currentValueWidth, healthRect.sizeDelta.y);
    }
}
