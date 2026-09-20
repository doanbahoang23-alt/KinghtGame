
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerStamina playerStamina;
    private PlayerSpeed playerSpeed;

    private KnockbackReceiver knockbackReceiver;
    private PlayerHealth playerHealth;

    private bool isSprinting = false;
    private bool isAttacking = false;

    private Animator animator;
    private Vector2 moveInput;
    private Vector2 rawMoveInput;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStamina = GetComponent<PlayerStamina>();
        animator = GetComponent<Animator>();
        playerSpeed = GetComponent<PlayerSpeed>();
        knockbackReceiver = GetComponent<KnockbackReceiver>();
        playerHealth = GetComponent<PlayerHealth>();
    }
    void FixedUpdate()
    {
        if (playerHealth != null && playerHealth.isDeath)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        if (knockbackReceiver != null && knockbackReceiver.IsKnockback) return;

        SetAnimatorMove();
        HandleStamina();
        HandleSpeed();
    }

    private void OnMove(InputValue inputValue)
    {
        rawMoveInput = inputValue.Get<Vector2>();
    }

    private void SetAnimatorMove()
    {
        moveInput = rawMoveInput;

        if (!isAttacking)
        {
            Flip();
        }

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
            if (!isAttacking)
            {
                animator.SetFloat("LastInputX", Mathf.RoundToInt(moveInput.normalized.x));
                animator.SetFloat("LastInputY", Mathf.RoundToInt(moveInput.normalized.y));
            }

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
        Vector3 currentScale = transform.localScale;
        if (moveInput.x > 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        else if (moveInput.x < 0)
        {
            currentScale.x = -Mathf.Abs(currentScale.x);
        }
        else if (moveInput.y != 0)
        {
            currentScale.x = Mathf.Abs(currentScale.x);
        }
        transform.localScale = currentScale;
    }

    private void HandleSpeed()
    {
        float speedFinal = playerSpeed.MoveSpeed(isSprinting);
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
        if (playerHealth != null && playerHealth.isDeath) return;
        if (inputValue.isPressed && !isAttacking)
        {
            animator.SetTrigger("Attack");
            isAttacking = true;
        }
    }

    public void ResetAttackState()
    {
        isAttacking = false;
    }


}
