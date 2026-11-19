using UnityEngine;
using Unity.Cinemachine;

public class Shotgun : WeaponBase
{
    

    [SerializeField] private float anguloDispersion = 10f; 

    public override void Shoot()
    {
        if (Time.time >= nextShootTime)
        {

            float exactAngle = GetMouseAngle();
           
            InstancePellet(exactAngle - anguloDispersion / 2f); 
            InstancePellet(exactAngle); 
            InstancePellet(exactAngle + anguloDispersion / 2f);

            DispararEfectos();
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