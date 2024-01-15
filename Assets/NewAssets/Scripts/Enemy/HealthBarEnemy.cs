using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class HealthBarEnemy : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text heathText;
    public Image fillImage;
    private Transform enemyTransform;

    Damage knightDamage;
    // public Vector3 offset = new(0, 0, 0);

    void Awake()
    {
        knightDamage = GameObject.FindGameObjectWithTag("Boss").GetComponent<Damage>();
        healthSlider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercent(knightDamage.currentHealth, knightDamage.maxHealth);
        heathText.text = knightDamage.currentHealth + "/" + knightDamage.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        float fillValue = CalculateSliderPercent(knightDamage.currentHealth, knightDamage.maxHealth);
        healthSlider.value = fillValue;
        heathText.text = knightDamage.currentHealth + "/" + knightDamage.maxHealth;
        // transform.localPosition += offset;
        // UpdateHealthBarPosition();
    }

    public float CalculateSliderPercent(float currentHealth, int maxHealth)
    {
        return ((float)currentHealth / (float)maxHealth);
    }

}
