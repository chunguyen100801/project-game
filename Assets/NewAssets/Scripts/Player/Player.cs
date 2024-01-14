using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public TMP_Text arrowCountText;
    Vector2 currentInput;
    public float walkSpeed = 5.0f;
    public float gravityScale = 10f;
    // private bool moving;
    Animator animator;
    private float currentSpeed = 0.0f;
    private bool isRunning = false;
    public float runSpeed = 7.0f;
    public float jumpForce = 8f;
    public float attackMoveSpeed = 2.0f;

    public bool isAlive = true;

    // Vector2 lookDirection = new Vector2(0, 0);

    BoxCollider2D touchState;
    public bool lockMoving = false;
    Damage damage;

    public ContactFilter2D castFilter;
    public float groundDistance = 0.001f, wallDistance = 0.2f;

    private bool isGrounded;
    public GameObject arrowPrefab;

    bool canMove = true;
    private bool isShooting = false;

    private bool isAttacking = false;

    public int arrowCount = 0;

    void Awake()
    {
        touchState = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damage = GetComponent<Damage>();
    }
    // Start is called before the first frame update
    void Start()
    {
        CenterOnScreen();
    }

    // Update is called once per frame
    void Update()
    {
        //   Vector3 playerPosition = launchPoint.position;

        //     // Tính toán vị trí mới cho điểm con
        //     Vector3 newPosition = new Vector3(playerPosition.x + offsetX, playerPosition.y + offsetY, playerPosition.z);

        //     // Gán vị trí mới cho điểm con
        //     launchPoint.position = newPosition;
    }



    void FixedUpdate()
    {
        lockMoving = damage.lockMoving;
        if (canMove)
        {
            if (!isAttacking)
            {
                if (!lockMoving)
                {
                    rigidbody2D.velocity = new Vector2(currentInput.x * currentSpeed, rigidbody2D.velocity.y);
                }
            }
            else
            {
                rigidbody2D.velocity = new Vector2(currentInput.x * attackMoveSpeed, rigidbody2D.velocity.y);
            }
        }
        isAlive = damage.isAlive;

        if (currentInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(currentInput.x), 1f, 1f);
        }
        animator.SetFloat("yVelocity", rigidbody2D.velocity.y);
        UpdateState(Vector2.down, groundDistance, ref isGrounded, "isGrounded");
    }

    void SetMovement(bool isMoving, float speed)
    {
        animator.SetBool("moving", isMoving);
        currentSpeed = speed;
        // if (isOnWall && !isGrounded)
        // {
        //     currentSpeed = 0;
        // }
        // else
        // {

        // }

    }

    void SetRunning(bool isRunning)
    {
        animator.SetBool("running", isRunning);
        currentSpeed = isRunning ? runSpeed : walkSpeed;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (damage.isAlive)
            {
                currentInput = context.ReadValue<Vector2>();
                if (isRunning)
                {
                    SetMovement(true, runSpeed);
                }
                else
                {
                    SetMovement(true, walkSpeed);
                }

            }
            else
            {
                currentSpeed = 0;
            }
        }
        else if (context.canceled)
        {
            currentInput = Vector2.zero;
            SetMovement(false, 0f);
        }
    }

    public void OnRunning(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRunning = true;
            SetRunning(true);

        }
        else if (context.canceled)
        {
            isRunning = false;
            SetRunning(false);

        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            animator.SetTrigger("jump");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
        }
        else
        {
            animator.ResetTrigger("jump");
        }
    }
    void UpdateState(Vector2 direction, float castLength, ref bool state, string paramName)
    {
        state = touchState.Cast(direction, castFilter, new RaycastHit2D[10], castLength) > 0;
        animator.SetBool(paramName, state);
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rigidbody2D.velocity = new Vector2(knockback.x, rigidbody2D.velocity.y + knockback.y);

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
            isAttacking = true;
            StartCoroutine(ResetSpeedAfterDelay());
        }
    }
    private IEnumerator ResetSpeedAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        isAttacking = false;

    }
    public void OnShootBow(InputAction.CallbackContext context)
    {
        if (arrowCount > 0)
        {
            if (context.started)
            {
                animator.SetTrigger("shootBow");
                canMove = false;
                isShooting = true;

                StartCoroutine(ResetCanMoveAfterDelay());
            }
            else if (context.canceled)
            {
                isShooting = false;
            }
        }
    }

    private IEnumerator ResetCanMoveAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);

        if (!isShooting)
        {
            canMove = true;
        }
    }
    void CenterOnScreen()
    {
        // Lấy kích thước của màn hình
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Tính toán vị trí trung tâm của màn hình trong không gian của Camera
        Vector3 centerScreen = new Vector3(screenWidth / 2, screenHeight / 2, 0);

        // Chuyển đổi vị trí trung tâm của màn hình thành vị trí trong không gian thế giới (world space)
        Vector3 centerWorld = Camera.main.ScreenToWorldPoint(centerScreen);

        // Đặt vị trí của Player tại vị trí trung tâm của màn hình
        transform.position = new Vector3(centerWorld.x, centerWorld.y, transform.position.z);
    }
    public Transform launchPoint;
    public void Launch()
    {
        GameObject arrow = Instantiate(arrowPrefab, launchPoint.position, arrowPrefab.transform.rotation);
        Vector3 originalScale = arrow.transform.localScale;
        arrow.transform.localScale = new Vector3(
            originalScale.x * transform.localScale.x > 0 ? 1 : -1, originalScale.y, originalScale.z);
        arrowCount--;
        arrowCountText.text = arrowCount.ToString();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
