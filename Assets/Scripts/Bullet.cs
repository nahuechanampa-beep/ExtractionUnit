using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bSpeed = 20f;
    public float lifeTime = 2f;
    public float dmg = 10;

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
        if (rb != null)
        {
            rb.linearVelocity = transform.right * bSpeed;
        }
    }



    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player")) return;

        if (collision.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(dmg);
            Destroy(gameObject);
        }
        else if (!collision.isTrigger) { Destroy(gameObject); }
    }

   
}