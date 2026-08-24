using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float chaseRadius = 5f;
    private Transform playerTarget;

    public bool HasTarget { get; private set; }
    public Vector2 DirectionToTarget { get; private set; }

    private void FixedUpdate()
    {
        if (playerTarget == null)
        {
            GetPlayerCoordinates();
        }
        FindPlayer();
    }

    public void GetPlayerCoordinates()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTarget = player.transform;
        }
    }

    public void FindPlayer()
    {
        if (playerTarget == null)
        {
            HasTarget = false;
            DirectionToTarget = Vector2.zero;
            return;
        }

        float distance = Vector2.Distance(transform.position, playerTarget.position);

        if (distance <= chaseRadius)
        {
            HasTarget = true;
            DirectionToTarget = (playerTarget.position - transform.position).normalized;
        }
        else
        {
            HasTarget = false;
            DirectionToTarget = Vector2.zero;
        }
    }
}