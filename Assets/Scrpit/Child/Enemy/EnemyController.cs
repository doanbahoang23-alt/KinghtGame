using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private EnemySpeed enemySpeed;
    private EnemyHealth enemyHealth;
    private EnemyAttackDamage enemyAttackDamage;
    EnemyVision enemyVision;
    private float pauseTimer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemySpeed = GetComponent<EnemySpeed>();
        enemyHealth = GetComponent<EnemyHealth>();
        enemyAttackDamage = GetComponent<EnemyAttackDamage>();
        enemyVision = GetComponent<EnemyVision>();
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
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.fixedDeltaTime;
            return;
        }
        FindPlayerToChase();
    }

    private void FindPlayerToChase()
    {
        if (enemyVision.HasTarget)
        {
            Vector2 move = enemyVision.DirectionToTarget;
            rb.linearVelocity = move * enemySpeed.MoveSpeed(false);
            HandleAnimation(move, true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            HandleAnimation(Vector2.zero, false);
        }
    }

    private void HandleAnimation(Vector2 dir, bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            if (dir.x > 0)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            }
            else if (dir.x < 0)
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            }
        }
    }

    public void PauseMovement(float time)
    {
        pauseTimer = time;
        rb.linearVelocity = Vector2.zero; // Thắng gấp (Phanh lại)
    }

}
