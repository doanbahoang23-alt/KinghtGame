
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private PlayerStamina playerStamina;
    private Animator animator;
    private Vector2 moveInput;
    private bool isSprinting = false;

    void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        playerStamina = GetComponent<PlayerStamina>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        HandleStamina();
        characterMovement.Move(moveInput, isSprinting);
    }

    private void OnMove(InputValue inputValue)
    {
        Vector2 checkInput = inputValue.Get<Vector2>();
        if (checkInput == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        else
        {
            UpdateAnimatorState(isSprinting);
        }
        moveInput = checkInput;
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
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
}
