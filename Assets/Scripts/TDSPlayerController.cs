using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TDSPlayerController : MonoBehaviour
{

    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;

    private IWeapon currentWeapon;
    private IInteractable currentInteractable;
    private SpriteRenderer playerSpriteRenderer;


    private void Awake()
    {
        currentWeapon = GetComponentInChildren<IWeapon>();

    }

    private void Start()
    {

        
    }


    private void Update()
    {
        if (currentWeapon == null) return;

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
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IInteractable>(out IInteractable interactable) && interactable == currentInteractable)
        {
            currentInteractable = null;
        }
    }

}

     


