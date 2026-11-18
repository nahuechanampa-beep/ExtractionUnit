using UnityEngine;

public class Shotgun : WeaponBase
{
   
    
    [SerializeField] private float anguloDispersion = 10f; // Ángulo total del cono de disparo

    public override void Shoot()
    {
        if (Time.time >= nextShootTime)
        {

            float exactAngle = GetMouseAngle();
           
            InstancePellet(exactAngle - anguloDispersion / 2f); // Disparo a la izquierda
            InstancePellet(exactAngle); // Disparo al centro
            InstancePellet(exactAngle + anguloDispersion / 2f); // Disparo a la derecha



            //Transform bulletTransform = Instantiate(pfBulletProjectile, spawnBulletPosition.position, pfBulletProjectile.rotation);
            //if (bulletTransform.TryGetComponent<Bullet>(out Bullet bullet))
            //{
            //        bullet.Setup(damage, bulletSpeed);
            //}


            ReproducirSonidoDisparo();
            nextShootTime = Time.time + fireRate;
        }
    }


    private void InstancePellet(float angle)
    {
        
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
        Transform bulletTransform = Instantiate(pfBulletProjectile, spawnBulletPosition.position, rotation);
        if (bulletTransform.TryGetComponent<Bullet>(out Bullet bullet))
        {
            bullet.Setup(damage, bulletSpeed);
        }
    }

}