using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{

    [SerializeField] protected Transform pfBulletProjectile;
    [SerializeField] protected Transform spawnBulletPosition;



    [SerializeField] protected float fireRate = 0.1f; // Tiempo entre disparos en segundos
    [SerializeField] protected float bulletSpeed = 20f; // Velocidad de la bala
    [SerializeField] protected float damage = 1f; // Daño de la bala
    public bool isAutomatic = false;
 

    [SerializeField] private AudioSource audioSource; // Fuente de audio para el sonido de disparo
    [SerializeField] private AudioClip soundDisparo;



    [SerializeField] private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer del arma



    [Header("Configuración de Audio (Bloque C)")]
    [SerializeField] private AudioClip sonidoDisparo;
    [SerializeField] private float pitchMin = 0.92f;
    [SerializeField] private float pitchMax = 1.08f;


  
    public GameObject WeaponGameObject => this.gameObject;
    public float FireRate => this.fireRate;
    public virtual bool IsAutomatic => this.isAutomatic;

    protected float nextShootTime = 0f;

    public virtual void Shoot()
    {
        

        if (audioSource != null && soundDisparo != null)
        {
            audioSource.PlayOneShot(soundDisparo);
        }

        if (Time.time >= nextShootTime + fireRate)
        {

            // Dentro de Shoot(), justo antes de Instantiate:
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;

            // Dirección exacta desde la punta del arma hacia el cursor
            Vector2 direccionAlMouse = (mousePos - spawnBulletPosition.position).normalized;

            // Calculamos el ángulo (si tu bala avanza con transform.right; resta 90f si avanza con transform.up)
            float angulo = Mathf.Atan2(direccionAlMouse.y, direccionAlMouse.x) * Mathf.Rad2Deg;
            Quaternion rotacionExacta = Quaternion.Euler(0f, 0f, angulo);

            // Instanciamos la bala usando esta rotación corregida
            //Instantiate(bulletPrefab, spawnBulletPoint.position, rotacionExacta);
            Transform bullet = Instantiate(pfBulletProjectile, spawnBulletPosition.position, rotacionExacta);

            //Instantiate(pfBulletProjectile, spawnBulletPosition.position, spawnBulletPosition.rotation);
            nextShootTime = Time.time;
        }


        ReproducirSonidoDisparo();
    }

    protected virtual void ReproducirSonidoDisparo()
    {
        if (audioSource != null && sonidoDisparo != null)
        {
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            audioSource.PlayOneShot(sonidoDisparo);
        }
    }

}
