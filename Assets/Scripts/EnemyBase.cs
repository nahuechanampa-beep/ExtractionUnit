using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float health = 100f;
    //[SerializeField] private float damage = 10f;
    //[SerializeField] private float attackRange = 0f;
    //[SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource audioSource;
     
    private float lastAttackTime;



    private void Update()
    {
        if (playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            //if (distanceToPlayer <= attackRange)
            //{
            //    AttackPlayer();
            //}
            //else
            //{
             FollowPlayer();
            //}
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerTransform = collision.transform;

            //AttackPlayer();
            FollowPlayer();
        }
    }


    //private void AttackPlayer()
    //{
    //    if (Time.time - lastAttackTime >= attackCooldown)
    //    {
    //        if (playerTransform != null && Vector2.Distance(transform.position, playerTransform.position) <= attackRange)
    //        {
    //            // Aquí puedes implementar la lógica para infligir daño al jugador
    //            Debug.Log($"El enemigo {name} ha atacado al jugador y le ha infligido {damage} de daño.");
    //            lastAttackTime = Time.time;
    //        }
    //    }
    //}

    private void FollowPlayer()
    {
        if (playerTransform != null)
        {
            Vector2 direction = playerTransform.position - transform.position;
            transform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
        }
    }

}
