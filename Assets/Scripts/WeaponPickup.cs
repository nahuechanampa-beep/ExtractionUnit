using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(WeaponBase))]
public class WeaponPickup : MonoBehaviour, IInteractable
{
    private WeaponBase weaponComponent;
    private PlayerCombat playerCombat;
    private bool playerInZone = false;

    private void Awake()
    {
        // Toma automáticamente el componente de arma (Shotgun, Rifle, etc.) puesto en este mismo objeto
        weaponComponent = GetComponent<WeaponBase>();
    }

    private void Update()
    {
        if (playerInZone && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (playerCombat == null) return;

        // 1. Muda este objeto adentro del jugador en la Hierarchy
        transform.SetParent(playerCombat.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        // 2. Apaga el dibujo del piso y el trigger para que desaparezca visualmente
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
        {
            sr.enabled = false;
        }

        if (TryGetComponent<Collider2D>(out Collider2D col))
        {
            col.enabled = false;
        }

        // 3. Registra y equipa el arma en el jugador
        playerCombat.UnlockAndEquipWeapon(weaponComponent);

        // 4. Desactiva este script de pickup para que no vuelva a ejecutarse
        playerInZone = false;
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            playerCombat = other.GetComponent<PlayerCombat>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            playerCombat = null;
        }
    }
}