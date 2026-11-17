using UnityEngine;

public abstract class WeaponBase : MonoBehaviour, IWeapon
{
    [Header("Bullet Prefab")]
    [SerializeField] protected Transform pfBulletProjectile;
    [SerializeField] protected Transform spawnBulletPosition;


    [Header("Weapon Stats")]
    [SerializeField] protected float fireRate = 0.1f; 
    [SerializeField] protected float bulletSpeed = 20f; 
    [SerializeField] protected float damage = 1f; 
    public bool isAutomatic = false;

    [Header("Player Sprite with this Weapon")]
    [SerializeField] private Sprite playerWeaponSprite;

    [Header("Audio Configuration")]
    [SerializeField] private AudioSource audioSource; 
    [SerializeField] private AudioClip soundDisparo;
    [SerializeField] private AudioClip sonidoDisparo;
    [SerializeField] private float pitchMin = 0.92f;
    [SerializeField] private float pitchMax = 1.08f;


  
    public GameObject WeaponGameObject => this.gameObject;
    public float FireRate => this.fireRate;
    public virtual bool IsAutomatic => this.isAutomatic;
    public Sprite PlayerWeaponSprite => this.playerWeaponSprite;

    protected float nextShootTime = 0f;

    public virtual void Shoot()
    {

        if (Time.time >= nextShootTime + fireRate)
        {

           
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            
            Vector2 direccionAlMouse = (mousePos - spawnBulletPosition.position).normalized;
            
            float angulo = Mathf.Atan2(direccionAlMouse.y, direccionAlMouse.x) * Mathf.Rad2Deg;
            Quaternion rotacionExacta = Quaternion.Euler(0f, 0f, angulo);
           
            Transform bulletTransform = Instantiate(pfBulletProjectile, spawnBulletPosition.position, rotacionExacta);
            if (bulletTransform.TryGetComponent<Bullet>(out Bullet bullet))
            {
                bullet.Setup(damage, bulletSpeed);
            }

            ReproducirSonidoDisparo();
            nextShootTime = Time.time;
        }
 
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
