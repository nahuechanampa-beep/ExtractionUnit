using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{


    [Header("Inventario de Armas")]
    [SerializeField] private List<GameObject> allWeaponsInHand;
    private IWeapon weapon;
    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        weapon = GetComponentInChildren<IWeapon>();

        if (weapon == null)
        {
            Debug.LogError("No se encontró ningún componente que implemente IWeapon.");
        }

        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void Start()
    {

        if (allWeaponsInHand.Count > 0 && allWeaponsInHand[0] != null)
        {
            IWeapon defaultWeapon = allWeaponsInHand[0].GetComponent<IWeapon>();
            if (defaultWeapon != null)
            {
                EquipWeapon(defaultWeapon);
            }
        }
        else
        {
            Debug.LogError("No hay armas configuradas en la lista 'allWeaponsInHand'.");
        }
    }


    private void Update()
    {
        
    }



    public void EquipWeapon(IWeapon newWeapon)
    {
        if (newWeapon == null)
        {
            Debug.LogError("El arma proporcionada es nula.");
            return;
        }
        if (weapon != null)
        {
            weapon.WeaponGameObject.SetActive(false);
        }
        weapon = newWeapon;
        weapon.WeaponGameObject.SetActive(true);

        if (playerSpriteRenderer != null && weapon.PlayerWeaponSprite != null)
        {
            playerSpriteRenderer.sprite = weapon.PlayerWeaponSprite;
        }
    }

    private void EquiparShotGun()
    {

        if (allWeaponsInHand.Count > 1 && allWeaponsInHand[1] != null)
        {
            IWeapon shotgun = allWeaponsInHand[1].GetComponent<IWeapon>();
            if (shotgun != null) EquipWeapon(shotgun);
        }
    }

    private void EquiparRifle()
    {

        if (allWeaponsInHand.Count > 2 && allWeaponsInHand[2] != null)
        {
            IWeapon rifle = allWeaponsInHand[2].GetComponent<IWeapon>();
            if (rifle != null) EquipWeapon(rifle);
        }
    }

}