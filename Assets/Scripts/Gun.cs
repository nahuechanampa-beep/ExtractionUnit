using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.Mathematics;
public class Gun : MonoBehaviour
{
   [SerializeField] private int Damage;
    [SerializeField] private float shootingCooldown;
    [SerializeField] private float spread;
    [SerializeField] private float reloadTime;
    [SerializeField] private int shootForce;

    [SerializeField] private int magazineSize;
    [SerializeField] private bool isAutomatic;
    [SerializeField] private bool AutoReload;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] LayerMask whatIsEnemy;

    private float amountOfSpread;

    private int bulletsLeft;

    private bool shooting;
    private bool reloading;
    private bool readyToShoot;











    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAutomatic)
        {
            shooting = Input.GetButton("Fire1");
        }
        else
        {
            shooting = Input.GetButtonDown("Fire1");
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (AutoReload && bulletsLeft <= 0 && !reloading)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
    }

  

    private void Shoot()
    {
        readyToShoot = false;
        amountOfSpread = UnityEngine.Random.Range(-spread, spread);
        
        //Spread
        Quaternion rotAfterSpread = Quaternion.Euler(firePoint.position.x, firePoint.position.y, firePoint.position.z + amountOfSpread);

        //Spawn bullet
        GameObject bulletCopy = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bulletCopy.transform.rotation = rotAfterSpread;
        bulletCopy.GetComponent<Rigidbody2D>().AddForce(bulletCopy.transform.right * shootForce, ForceMode2D.Impulse);

        bulletsLeft--;
        Invoke("ResetShot", shootingCooldown);
    }


    private void ResetShot()
    {
        readyToShoot = true;
    }


    private void Reload()
    {
        reloading = true;
        Invoke("FinishReload", reloadTime);
    }

    private void FinishReload()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

}
