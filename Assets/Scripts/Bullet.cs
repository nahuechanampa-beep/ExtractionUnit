using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bSpeed = 20f;
    public float lifeTime = 2f;
    public float dmg = 10;
    [Header("VFX")]
    [SerializeField] private GameObject impactParticlesPrefab;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        

        rb.linearVelocity = transform.right * bSpeed;
        Destroy(gameObject, lifeTime);
    }


    public void Setup(float weaponDamage, float weaponSpeed)
    {
        this.dmg = weaponDamage;
        this.bSpeed = weaponSpeed;
      
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) return;

        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(dmg);
            SpawnImpactParticles();
            Destroy(gameObject);
        }
        else if (!collision.isTrigger) 
        {
            SpawnImpactParticles();
            Destroy(gameObject);
        }
    }

    private void SpawnImpactParticles()
    {
        if (impactParticlesPrefab != null)
        {
            Instantiate(impactParticlesPrefab, transform.position, Quaternion.identity);
        }
    }
}