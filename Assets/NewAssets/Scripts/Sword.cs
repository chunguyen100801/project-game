using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public int swordDamage = 100;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();
        float swordDirection;
        try
        {
            swordDirection = ((transform.parent).parent).parent.localScale.x;
        }
        catch
        {
            swordDirection = -(transform.parent.localScale.x);
        }
        if (damage != null)
        {
            Vector2 deliveredKnockback = swordDirection > 0 ? knockback * new Vector2(-1, 1) : knockback;
            damage.Hit(swordDamage, deliveredKnockback);
        }


    }
}
