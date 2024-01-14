using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    public UnityEvent<int, int> healthChanged;
    public UnityEvent damageableDeath;
    public UnityEvent<int, Vector2> damageableHit;
    public GameObject popUpText;
    public GameObject floatingTextPrefab;
    private Animator animator;

    public int maxHealth = 1000;
    public int currentHealth = 1000;
    public bool isAlive = true;

    public bool checkwinner = false;


    public bool isInvincible = false;

    public float timeInvincible = 1f;

    public bool lockMoving = false;
    float invincibleTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            // Debug.Log(invincibleTimer);
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                lockMoving = false;
            }
        }
    }



    public bool Hit(int damage, Vector2 knockback)
    {

        if (isAlive && !isInvincible)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                isAlive = false;
                animator.SetBool("isAlive", isAlive);
                checkwinner = true;
                damageableDeath.Invoke();
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("hurt");

            ShowHealthText(damage, false);
            lockMoving = true;
            damageableHit?.Invoke(damage, knockback);

            return true;
        }

        return false;
    }

    void ShowHealthText(int damage, bool isHealth)
    {
        var go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        TextMesh textMesh = go.GetComponent<TextMesh>();

        if (isHealth == true)
        {
            textMesh.text = damage.ToString();
            textMesh.color = Color.green;
        }
        else
        {
            go.GetComponent<TextMesh>().text = damage.ToString();
        }


        // Đặt màu xanh cho văn bản trực tiếp
    }



    public bool Heal(int healthRestore)
    {
        if (isAlive && currentHealth < maxHealth)
        {
            int maxHeal = Mathf.Max(maxHealth - currentHealth, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            currentHealth += actualHeal;
            ShowHealthText(actualHeal, true);
            healthChanged?.Invoke(currentHealth, maxHealth);
            animator.SetBool("isAlive", true);
            return true;
        }

        return false;
    }
    // Phương thức này để gán giá trị cho event DamageableHit
    public void SetDamageableHitEvent(UnityAction<int, Vector2> action)
    {
        damageableHit.AddListener(action);
    }


}
