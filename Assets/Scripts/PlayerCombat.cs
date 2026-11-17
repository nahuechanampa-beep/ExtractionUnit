using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private WeaponBase startingWeapon; // Tu componente Pistol del Player

    private List<IWeapon> unlockedWeapons = new List<IWeapon>();
    private IWeapon currentWeapon;

    private void Awake()
    {
        if (startingWeapon != null)
        {
            UnlockAndEquipWeapon(startingWeapon);
        }
    }

    private void Update()
    {
        // Cambio de arma con 1, 2, 3 según el orden en que se recogieron
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipWeapon(2);

        if (currentWeapon == null) return;

        bool fireInput = currentWeapon.IsAutomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
        if (fireInput)
        {
            currentWeapon.Shoot();
        }
    }

    public void UnlockAndEquipWeapon(IWeapon newWeapon)
    {
        if (!unlockedWeapons.Contains(newWeapon))
        {
            unlockedWeapons.Add(newWeapon);
        }
        EquipWeapon(unlockedWeapons.IndexOf(newWeapon));
    }

    private void EquipWeapon(int index)
    {
        if (index >= 0 && index < unlockedWeapons.Count)
        {
            currentWeapon = unlockedWeapons[index];

            if (playerSpriteRenderer != null && currentWeapon.PlayerWeaponSprite != null)
            {
                playerSpriteRenderer.sprite = currentWeapon.PlayerWeaponSprite;
            }
        }
    }
}