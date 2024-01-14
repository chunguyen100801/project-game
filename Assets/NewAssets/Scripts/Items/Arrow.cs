using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    public int arrowDamage = 200;
    public Vector2 moveSpeed = new(9f, 0);
    public Vector2 knockback = Vector2.zero;
    void Start()
    {
        rigidbody2d.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);

    }

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(transform.position.magnitude);
        if (transform.position.magnitude > transform.position.x + 40.0f)
        {
            Destroy(gameObject);
        }

    }


    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage damage = collision.GetComponent<Damage>();
        if (damage != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            damage.Hit(arrowDamage, deliveredKnockback);
            Destroy(gameObject);
        }


    }
}
