using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TDSPlayerController : MonoBehaviour
{

    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private GameObject interactPromptUI;
    
    private IInteractable currentInteractable;
    private SpriteRenderer playerSpriteRenderer;
    private PlayerInventory playerInventory;

    private void Awake()
    {
        
        playerInventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        if (interactPromptUI != null)
        {
            interactPromptUI.SetActive(false);
        }

    }


    private void Update()
    {
        WeaponChange();
        HandleShooting();
        HandleInteractions();
    }

    


    private void HandleShooting()
    {
        if (playerInventory == null || playerInventory.CurrentWeapon == null) return;

        IWeapon currentWeapon = playerInventory.CurrentWeapon;

        if (currentWeapon.IsAutomatic)
        {
            if (Input.GetButton("Fire1"))
            {
                currentWeapon.Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                currentWeapon.Shoot();
            }
        }
    }

    private void WeaponChange()
    {
        if (playerInventory == null) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInventory.EquipWeaponByIndex(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerInventory.EquipWeaponByIndex(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerInventory.EquipWeaponByIndex(2);
        }
    }



    private void HandleInteractions()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            currentInteractable = interactable;
            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
            if (interactPromptUI != null)
            {
                interactPromptUI.SetActive(false);
            }
        }
    }


}

     


