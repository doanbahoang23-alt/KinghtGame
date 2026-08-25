
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStamina playerStamina;
    private PlayerSpeed playerSpeed;
    private PlayerAttackDamage playerAttackDamage;
    private PlayerHealth playerHealth;

    [SerializeField] private float knockbackTime = 0.2f;
    private float knockbackCounter;
    private bool isSprinting = false;

    private Animator animator;
    private Vector2 moveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = GetComponent<PlayerStamina>();
        animator = GetComponent<Animator>();
        playerSpeed = GetComponent<PlayerSpeed>();
        playerAttackDamage = GetComponent<PlayerAttackDamage>();
        playerHealth = GetComponent<PlayerHealth>();

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (knockbackCounter > 0)
        {
            knockbackCounter -= Time.fixedDeltaTime;
            return;
        }
        HandleStamina();
        HandleSpeed();
    }

    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        Flip();
        if (moveInput == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
        else
        {
            UpdateAnimatorState(isSprinting);
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
    }


    private void OnSprint(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            isSprinting = !isSprinting;
            //isSprinting = inputValue.isPressed;
            if (moveInput != Vector2.zero)
            {
                UpdateAnimatorState(isSprinting);
            }

        }

    }

    private void UpdateAnimatorState(bool isRunning)
    {
        animator.SetBool("isWalking", !isRunning);
        animator.SetBool("isRunning", isRunning);
    }

    private void Flip()
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void HandleSpeed()
    {
        float speedFinal = playerSpeed.MoveSpeed(isSprinting);
        Debug.Log($"Vector Phím: {moveInput} | Tốc độ: {speedFinal}");
        rb.linearVelocity = moveInput.normalized * speedFinal;
    }

    private void HandleStamina()
    {
        if (isSprinting && moveInput != Vector2.zero)
        {
            float staminaRequired = playerStamina.Lost * Time.fixedDeltaTime;

            if (playerStamina.HasEnoughStamina(staminaRequired))
            {
                playerStamina.UseStamina(staminaRequired);
            }
            else
            {
                isSprinting = false;
                UpdateAnimatorState(false);
            }
        }
        else
        {
            playerStamina.RegenStamina(playerStamina.Regen * Time.fixedDeltaTime);
        }

    }

    private void OnAttack(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void TakeKnockback(Vector2 force)
    {
        knockbackCounter = knockbackTime;
        rb.linearVelocity = force;
    }

}
