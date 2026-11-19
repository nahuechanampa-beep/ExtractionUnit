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

    public IWeapon CurrentWeapon => weapon;

    private void Awake()
    {
        //weapon = GetComponentInChildren<IWeapon>();


        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
    }

    private void Start()
    {
        // Apagamos todas primero por si alguna quedó activa por error en la escena
        foreach (GameObject weaponObj in allWeaponsInHand)
        {
            if (weaponObj != null) weaponObj.SetActive(false);
        }

        // Equipamos la primera arma (índice 0 = Pistola)
        EquipWeaponByIndex(0);
    }


    private void Update()
    {
        
    }

    public void EquipWeaponByIndex(int index)
    {
        if (index < 0 || index >= allWeaponsInHand.Count || allWeaponsInHand[index] == null)
        {
            Debug.LogWarning($"No hay ningún arma asignada en el índice {index} de allWeaponsInHand.");
            return;
        }

        IWeapon selectedWeapon = allWeaponsInHand[index].GetComponent<IWeapon>();
        if (selectedWeapon != null)
        {
            EquipWeapon(selectedWeapon);
        }
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

    //private void EquiparShotGun()
    //{

    //    if (allWeaponsInHand.Count > 1 && allWeaponsInHand[1] != null)
    //    {
    //        IWeapon shotgun = allWeaponsInHand[1].GetComponent<IWeapon>();
    //        if (shotgun != null) EquipWeapon(shotgun);
    //    }
    //}

    //private void EquiparRifle()
    //{

    //    if (allWeaponsInHand.Count > 2 && allWeaponsInHand[2] != null)
    //    {
    //        IWeapon rifle = allWeaponsInHand[2].GetComponent<IWeapon>();
    //        if (rifle != null) EquipWeapon(rifle);
    //    }
    //}

}