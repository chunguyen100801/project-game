using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    public int attackDamage = 200;
    public Vector2 knockback = Vector2.up;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit 
        Damage damageable = collision.GetComponent<Damage>();

        if (damageable != null)
        {
            damageable.Hit(attackDamage, knockback);
            animator.SetTrigger("bomb");
            Destroy(gameObject, 1.0f);

        }
        else
        {
            Debug.Log("Damageable null");
        }
    }
}
