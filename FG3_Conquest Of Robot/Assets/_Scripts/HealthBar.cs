using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(float health)
    {
        this.slider.maxValue = health;
        this.slider.value = health;

        this.fill.color = this.gradient.Evaluate(1f);
    }

    public void SetCurrentHealth(float health)
    {
        this.slider.value = health;
        this.fill.color = this.gradient.Evaluate(this.slider.normalizedValue);
    }
}
