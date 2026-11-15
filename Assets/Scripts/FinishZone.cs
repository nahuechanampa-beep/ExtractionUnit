using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishZone : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject victoryPanel;
    private bool playerInZone = false;

    private void Update()
    {
        if (playerInZone && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interact();
        }
    }



    public void Interact()
    {
        Debug.Log("¡Has llegado a la zona de extracción!");
        victoryPanel.SetActive(true);
        Time.timeScale = 0f; 
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
        }
    }

}
