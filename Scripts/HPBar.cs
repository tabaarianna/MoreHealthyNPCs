using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    private Slider slider;
    public Image fill;

    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        GetComponentInParent<IHealth>().OnHPPctChanged += HandleHPPctChanged;
        GetComponentInParent<IHealth>().OnPoison += PoisonHPBar;
    }

    private void HandleHPPctChanged(float pct)
    {
        fill.color = Color.red;
        slider.value = pct;
    }
    private void PoisonHPBar(float pct)
    {
        fill.color = new Color(0f, .25f, 0.07f);
        slider.value = pct;
    }
}
