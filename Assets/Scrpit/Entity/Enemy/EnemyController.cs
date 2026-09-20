using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private EnemySpeed enemySpeed;
    private EnemyAttackDamage enemyAttackDamage;
    private EnemyVision enemyVision;

    private KnockbackReceiver knockbackReceiver;
    private float pauseTimer;
    [SerializeField] private float attackCooldown = 1.2f;
    private float attackTimer = 0f;
    private bool isTouchingTarget = false;

    [SerializeField] private float roamSpeedMultiplier = 0.5f;
    [SerializeField] private float minWaitTime = 1f;
    [SerializeField] private float maxWaitTime = 3f;
    [SerializeField] private float minRoamTime = 1f;
    [SerializeField] private float maxRoamTime = 2.5f;

    private float roamTimer;
    private bool isWaiting = true;
    private Vector2 roamDirection;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        enemySpeed = GetComponent<EnemySpeed>();
        enemyAttackDamage = GetComponent<EnemyAttackDamage>();
        enemyVision = GetComponent<EnemyVision>();
        knockbackReceiver = GetComponent<KnockbackReceiver>();
    }

    void FixedUpdate()
    {
        if (knockbackReceiver != null && knockbackReceiver.IsKnockback) return;
        if (pauseTimer > 0)
        {
            pauseTimer -= Time.fixedDeltaTime;
            return;
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.fixedDeltaTime;
        }
        HandleMovementAttack();
    }

    private void HandleMovementAttack()
    {
        if (isTouchingTarget)
        {
            rb.linearVelocity = Vector2.zero;
            HandleAnimation(Vector2.zero, false);
            return;
        }
        if (enemyVision.HasTarget)
        {
            isWaiting = true;
            Vector2 move = enemyVision.DirectionToTarget;
            rb.linearVelocity = move * enemySpeed.MoveSpeed(false);
            HandleAnimation(move, true);
        }
        else
        {
            HandleRoanming();
        }
    }

    private void HandleRoanming()
    {
        roamTimer -= Time.fixedDeltaTime;
        if (roamTimer <= 0)
        {
            isWaiting = !isWaiting;
            if (isWaiting)
            {
                roamTimer = Random.Range(minWaitTime, maxWaitTime);
            }
            else
            {
                roamTimer = Random.Range(minRoamTime, maxRoamTime);
                roamDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            }
        }
        if (isWaiting)
        {
            rb.linearVelocity = Vector2.zero;
            HandleAnimation(Vector2.zero, false);
        }
        else
        {
            rb.linearVelocity = roamDirection * (enemySpeed.MoveSpeed(false) * roamSpeedMultiplier);
            HandleAnimation(roamDirection, true);
        }
    }

    private void HandleAnimation(Vector2 dir, bool isMoving)
    {
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector3 currentScale = transform.localScale;
            if (dir.x > 0)
            {
                currentScale.x = Mathf.Abs(currentScale.x);
            }
            else if (dir.x < 0)
            {
                currentScale.x = -Mathf.Abs(currentScale.x);
            }
            transform.localScale = currentScale;
        }
    }

    public void PauseMovement(float time)
    {
        pauseTimer = time;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyAttackDamage.TargetTag))
        {
            isTouchingTarget = true;

            if (attackTimer <= 0f)
            {
                if (collision.gameObject.TryGetComponent(out IDamageable damageable))
                {
                    damageable.TakeDamage(enemyAttackDamage.AttackDamage());
                }

                if (collision.gameObject.TryGetComponent(out IKnockbackable knockbackableTarget))
                {
                    Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                    knockbackableTarget.ApplyKnockback(knockbackDir, enemyAttackDamage.KnockbackForce);
                }

                attackTimer = attackCooldown;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyAttackDamage.TargetTag))
        {
            isTouchingTarget = false;
        }
    }

    public void TakeKnockback(Vector2 force)
    {
        PauseMovement(0.5f);
        if (rb != null)
        {
            rb.linearVelocity = force;
        }
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        if (rb != null)
        {
            PauseMovement(0.3f);
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(direction * force, ForceMode2D.Impulse);
        }
    }

}
