using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text heathText;
    public Image fillImage;

    Damage playerDamage;
    public GameObject player;
    void Awake()
    {
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<Damage>();
        healthSlider = GetComponent<Slider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercent(playerDamage.currentHealth, playerDamage.maxHealth);
        heathText.text = playerDamage.currentHealth + "/" + playerDamage.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        float fillValue = CalculateSliderPercent(playerDamage.currentHealth, playerDamage.maxHealth);
        healthSlider.value = fillValue;
        heathText.text = playerDamage.currentHealth + "/" + playerDamage.maxHealth;
    }

    public float CalculateSliderPercent(float currentHealth, int maxHealth)
    {
        return ((float)currentHealth / (float)maxHealth);
    }
}
