using UnityEngine;

public class Rifle : WeaponBase
{
    private void Start()
    {

        fireRate = 0.5f;
        isAutomatic = true;
    }

   //private void Update()
   // {
   //     if (Input.GetMouseButton(0))
   //     {
   //         Shoot();
   //     }
   // }
}
