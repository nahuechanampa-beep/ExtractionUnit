using UnityEngine;

public interface IWeapon
{
    void Shoot();
    GameObject WeaponGameObject { get; }
    Sprite PlayerWeaponSprite { get; }
    float FireRate { get; }
    bool IsAutomatic { get; }
}
