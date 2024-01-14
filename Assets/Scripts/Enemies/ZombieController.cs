using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ZombieController : MonoBehaviour
{

    public float speed = 1.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public int currentHealth = 1000;
    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    // PlayerController playerController;
    Vector2 firstPosition;
    // Damageable zombieDamageable;
    // Damageable damageable;
    // Start is called before the first frame update
    //Damageable zombieDamageable;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        firstPosition = rigidbody2D.position;
        //zombieDamageable = GetComponent<Damageable>();
        // Gán giá trị cho event damageableHit
        // zombieDamageable.damageableHit.AddListener(OnZombieHit);

    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
    private bool isFacingRight = true;
    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;

            // Flip horizontally based on the direction and initial position
            if (position.x > firstPosition.x && !isFacingRight)
            {
                Flip();
            }
            else if (position.x < firstPosition.x && isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;

            // Flip horizontally based on the direction
            if (direction > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (direction < 0 && isFacingRight)
            {
                Flip();
            }
        }

        rigidbody2D.MovePosition(position);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the GameObject horizontally
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public int attackDamage = 50;
    public Vector2 knockback = Vector2.up;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit 
        Damage damageable = collision.GetComponent<Damage>();

        if (damageable != null)
        {
            //currentHealth -= 200;
            // Call Hit() on the damageable component of the collided object
            damageable.Hit(attackDamage, knockback);
            //zombieDamageable.Hit(200,knockback);


            // Check if Zombie's health is zero or below

            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // playerController.OnHit(1, Vector2.zero); // Giảm 1 sức khỏe khi va chạm
        }
    }
    // Hàm này sẽ được gọi khi event damageableHit được kích hoạt
    public void OnZombieHit(int damage, Vector2 knockback)
    {
        // Xử lý logic khi Zombie nhận được sát thương

    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rigidbody2D.velocity = new Vector2(knockback.x, rigidbody2D.velocity.y + knockback.y);
    }

}