using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float walkSpeed = 2f;

    Rigidbody2D rb;
    CapsuleCollider2D touchState;

    private Vector2 walkDirection = Vector2.left;

    public ContactFilter2D castFilter;
    Animator animator;
    public DetectionZone detectionZone;


    public float groundDistance = 0.05f, wallDistance = 0.2f;

    private bool isGrounded, isOnWall;
    public float roadLength = 10f;

    public bool hasTarget = false;
    private float currentSpeed;

    public Transform launchPoint;
    public GameObject arrowPrefab;

    public GameObject bulltet1;
    public GameObject bulltet2;
    public GameObject bulltet3;
    Damage damage;

    public GameObject canvasBoard;

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

        if (canvasBoard)
        {
            Debug.Log("abc" + canvasBoard.transform.localScale.x);

            if (transform.localScale.x < 0)
            {
                canvasBoard.transform.localScale = new Vector3(Mathf.Abs(canvasBoard.transform.localScale.x),
            canvasBoard.transform.localScale.y, canvasBoard.transform.localScale.z);
            }
            Debug.Log(canvasBoard.transform.localScale.x)
;
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

    public void Launch()
    {
        GameObject arrow = Instantiate(arrowPrefab, launchPoint.position, arrowPrefab.transform.rotation);
        Vector3 originalScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? -1 : 1, originalScale.y, originalScale.z);
    }

    public void LaunchBoss()
    {
        GameObject[] bulletList = new GameObject[] { bulltet1, bulltet2, bulltet3 };
        int randomIndex = UnityEngine.Random.Range(0, bulletList.Length);
        GameObject bulletRandom = bulletList[randomIndex];

        GameObject bullet = Instantiate(bulletRandom, launchPoint.position, bulletRandom.transform.rotation);

        Vector3 originalScale = bullet.transform.localScale;
        bullet.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? -1 : 1, originalScale.y, originalScale.z);
    }
}
