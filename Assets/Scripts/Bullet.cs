using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 20f;
    public float lifeTime = 2f;
    public int dmg = 10;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

       
        rb.linearVelocity = transform.right * velocidad; 

        
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log("TRIGGER DETECTADO CON: " + hitInfo.gameObject.name);

        if (hitInfo.CompareTag("Player")) return;


        Destroy(gameObject);
    }
}