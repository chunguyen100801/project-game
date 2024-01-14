using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public int attackDamage = 50;
    public Vector2 knockback = Vector2.up;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit 
        Damage damageable = collision.GetComponent<Damage>();

        if (damageable != null)
        {
            damageable.Hit(attackDamage, knockback);
        }
        else
        {
            Debug.Log("Damageable null");
        }
    }
}
