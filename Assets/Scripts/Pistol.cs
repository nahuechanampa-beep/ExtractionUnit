using UnityEngine;

public class Pistol : WeaponBase
{
   private void Start()
    {
       
        fireRate = 0.5f; 
        isAutomatic = false; 
    }

    //private void Update()
    //{
    //   if(Input.GetMouseButtonDown(0))
    //   {
    //        Shoot();
    //   }
    //}
    
}
