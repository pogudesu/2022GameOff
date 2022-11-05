using HealthSystem;
using UnityEngine;

public class UIHealth : MonoBehaviour
{
    [SerializeField]private GameObject healthForeground;
    private float maxWidth;
    private RectTransform healthRect;
    [SerializeField]private IndividualHealth health;
    private void Awake()
    {
        healthRect = healthForeground.GetComponent<RectTransform>();
        maxWidth = healthRect.sizeDelta.x;
        health.Initialize();
        OnChangedHealthPointValue(health.healthPoint.Value);
    }

    private void OnEnable()
    {
        health.healthPoint.OnValueChanged += OnChangedHealthPointValue;
    }

    private void OnDisable()
    {
        health.healthPoint.OnValueChanged -= OnChangedHealthPointValue;

    }

    private void OnChangedHealthPointValue(int value)
    {
        float percentage = (float)value / (float)health.maxHealthPoint.Value;
        float currentValueWidth = maxWidth * percentage;
        if (currentValueWidth < 0) return;
        healthRect.sizeDelta = new Vector2(currentValueWidth,healthRect.sizeDelta.y);
    }
}
