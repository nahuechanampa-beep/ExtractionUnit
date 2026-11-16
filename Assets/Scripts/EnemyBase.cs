using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] protected float health = 100f;
    //[SerializeField] private float damage = 10f;
    //[SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform target;

    private float lastAttackTime;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.updateRotation = false; // Desactivar la rotación automática del agente
        agent.updateUpAxis = false; // Desactivar la actualización del eje Y del agente

    }

    private void Update()
    {
       
        agent.SetDestination(target.position);

        if (agent.velocity.sqrMagnitude > 0.01f) // Solo rotar si realmente se está moviendo
        {
            Vector2 direccionMovimiento = agent.velocity.normalized;

            // Calcula el ángulo (usa - 90f si tu sprite mira hacia arriba por defecto, 
            // o quita el - 90f si tu sprite mira hacia la derecha por defecto)
            float angulo = Mathf.Atan2(direccionMovimiento.y, direccionMovimiento.x) * Mathf.Rad2Deg - 90f;

            // Aplica rotación únicamente en Z, dejando X e Y clavados en 0
            transform.rotation = Quaternion.Euler(0f, 0f, angulo);
        }








        //if (playerTransform != null)
        //{
        //    float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        //    //if (distanceToPlayer <= attackRange)
        //    //{
        //    //    AttackPlayer();
        //    //}
        //    //else
        //    //{
        //     //FollowPlayer();
        //    //}
        //}
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

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí puedes implementar la lógica para manejar la muerte del enemigo
        Debug.Log($"El enemigo {name} ha muerto.");
        Destroy(gameObject);
    }

}
