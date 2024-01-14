using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float walkStopRate = 0.04f;

    Rigidbody2D rb;
    CapsuleCollider2D touchState;

    private Vector2 walkDirection = Vector2.left;

    public ContactFilter2D castFilter;
    Animator animator;
    public DetectionZone detectionZone;


    public float groundDistance = 0.5f, wallDistance = 0.2f;

    private bool isGrounded, isOnWall;
    public float roadLength = 5f;

    public bool hasTarget = false;
    private float currentSpeed;
    Damage damage;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchState = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        animator = GetComponent<Animator>();
        damage = GetComponent<Damage>();

    }
    private float totalRoad = 0f;
    private void FixedUpdate()
    {
        if (damage.isAlive)
        {
            if (!damage.lockMoving)
            {
                if (!isGrounded && isOnWall)
                {
                    totalRoad = 0f;
                    walkSpeed = 0f;
                }
                if (isGrounded && isOnWall)
                {
                    totalRoad = 0f;
                    FlipDirection();
                }
                else
                {
                    totalRoad += rb.velocity.x * Time.deltaTime * currentSpeed;
                    if (totalRoad >= roadLength || totalRoad <= -roadLength)
                    {
                        totalRoad = 0f;
                        FlipDirection();
                    }

                    rb.velocity = new Vector2(currentSpeed * walkDirection.x, rb.velocity.y);
                }
            }

        }
        else
        {
            Destroy(gameObject, 2);
        }
        UpdateState(Vector2.down, groundDistance, ref isGrounded, "isGrounded");
        UpdateState(walkDirection, wallDistance, ref isOnWall, "isOnWall");
    }

    private void FlipDirection()
    {
        gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
        walkDirection.x *= -1;
    }

    void Update()
    {
        hasTarget = detectionZone.detectedColliders.Count > 0;
        animator.SetBool("hasTarget", hasTarget);
        if (damage.isAlive)
        {
            if (!hasTarget)
            {
                currentSpeed = walkSpeed;
            }
            else
            {
                currentSpeed = 0f;
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    void UpdateState(Vector2 direction, float castLength, ref bool state, string paramName)
    {
        state = touchState.Cast(direction, castFilter, new RaycastHit2D[10], castLength) > 0;
        animator.SetBool(paramName, state);
    }

    public void Winner()
    {
        animator.SetBool("isWin", true);
    }

}
