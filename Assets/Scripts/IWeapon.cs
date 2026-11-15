using UnityEngine;

public interface IWeapon
{
   void Shoot();
  

    float FireRate { get; }
    bool IsAutomatic { get; }
}
