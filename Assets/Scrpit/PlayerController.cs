using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float sprint = 1.5f; //tốc độ thêm 1.5 lần
    private float buffBonusSpeed = 0f;
    private bool isSprinting = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        PlayerSpeed();
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
            animator.SetBool("isWalking", !isSprinting);
            animator.SetBool("isRunning", isSprinting);
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
            if (moveInput != Vector2.zero)
            {
                animator.SetBool("isWalking", !isSprinting);
                animator.SetBool("isRunning", isSprinting);
            }

        }

    }

    private void PlayerSpeed()
    {
        float currentSpeed = baseMoveSpeed + buffBonusSpeed;
        if (isSprinting)
        {
            currentSpeed *= sprint;
        }
        rb.linearVelocity = new Vector2(moveInput.x * currentSpeed, moveInput.y * currentSpeed);
    }
}
