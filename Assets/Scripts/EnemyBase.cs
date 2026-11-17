using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform target;
    [SerializeField] private float detectionRadius = 2f;
   
    
    [SerializeField] protected float knockbackForce = 8f;
    [SerializeField] protected float knockbackDuration = 0.2f;
    [SerializeField] protected float attackPauseDuration = 1.0f;

    private float lastAttackTime;
    protected float currentHealth;
    protected bool isAttackPaused = false;
    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        
        agent.speed = moveSpeed;
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        currentHealth = maxHealth;

    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        
        if (distanceToPlayer <= detectionRadius)
        {

            detectionRadius = 100f;
            agent.SetDestination(target.position);
            

        }
     
        

        if (agent.velocity.sqrMagnitude > 0.01f) // Solo rotar si realmente se está moviendo
        {
            Vector2 direccionMovimiento = agent.velocity.normalized;

            // Calcula el ángulo (usa - 90f si tu sprite mira hacia arriba por defecto, 
            // o quita el - 90f si tu sprite mira hacia la derecha por defecto)
            float angulo = Mathf.Atan2(direccionMovimiento.y, direccionMovimiento.x) * Mathf.Rad2Deg - 90f;

            // Aplica rotación únicamente en Z, dejando X e Y clavados en 0
            transform.rotation = Quaternion.Euler(0f, 0f, angulo);
        }

    }

    


    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        
        Debug.Log($"El enemigo {name} ha muerto.");
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (isAttackPaused) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // 1. Aplica daño mediante la interfaz IDamageable
            if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.TakeDamage(attackDamage);
            }

            // 2. Empuja al jugador en dirección opuesta al zombie
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
            {
                Vector2 pushDir = (collision.transform.position - transform.position).normalized;
                playerHealth.ApplyKnockback(pushDir, knockbackForce, knockbackDuration);
            }

            // 3. Pausa al zombie por un momento
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        isAttackPaused = true;
        if (agent != null) agent.isStopped = true;

        yield return new WaitForSeconds(attackPauseDuration);

        if (agent != null && agent.isOnNavMesh) agent.isStopped = false;
        isAttackPaused = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }


}
