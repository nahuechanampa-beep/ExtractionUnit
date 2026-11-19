using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FinishZone : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject eToInteract;
    [SerializeField] private GameObject uiHUD;
    [SerializeField] private GameObject[] goldenStars; // Arrastrá StarFill_1, StarFill_2 y StarFill_3 acá
    


    public void Interact()
    {
        Debug.Log("¡Has llegado a la zona de extracción!");
        ShowVictoryPanel();
        Time.timeScale = 0f; 
    }

    public void ShowVictoryPanel()
    {
        victoryPanel.SetActive(true);
        eToInteract.SetActive(false);
        uiHUD.SetActive(false);
        int rescued = RescueManager.Instance != null ? RescueManager.Instance.RescuedCivilians : 0;

        for (int i = 0; i < goldenStars.Length; i++)
        {
            // Prende la estrella 0 si rescató >= 1, la 1 si >= 2, la 2 si >= 3
            goldenStars[i].SetActive(i < rescued);
        }
    }

    

}
