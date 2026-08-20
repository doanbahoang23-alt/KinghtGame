using System.Linq.Expressions;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float baseMoveSpeed = 5f;
    [SerializeField] private float sprint = 1.5f; //tốc độ thêm 1.5 lần
    private float buffBonusSpeed = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction, bool isSprinting = false)
    {
        float currentSpeed = baseMoveSpeed + buffBonusSpeed;
        if (isSprinting)
        {
            currentSpeed *= sprint;
        }

        rb.linearVelocity = new Vector2(direction.x * currentSpeed, direction.y * currentSpeed);
    }
}