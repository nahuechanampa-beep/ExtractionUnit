using UnityEngine;
using UnityEngine.UI;
using TMPro; // Borrá esta línea y lo de civiliansText si no usás TextMeshPro

public class HUDManager : MonoBehaviour
{
    [Header("UI Barra de Vida")]
    [SerializeField] private Image healthFillImage;

    [Header("Paneles de Fin de Juego")]
    [SerializeField] private GameObject defeatMenu;

    [Header("UI Civiles (Opcional)")]
    [SerializeField] private TextMeshProUGUI civiliansText;
    [SerializeField] private int totalCivilians = 3;

    [Header("Crosshair / Mira")]
    [SerializeField] private Texture2D crosshairTexture;

    private void OnEnable()
    {
        // Suscripción a los eventos estáticos de PlayerHealth (Patrón Observer)
        PlayerHealth.OnHealthChanged += UpdateHealthBar;
        PlayerHealth.OnPlayerDied += ShowDefeatMenu;

        // Suscripción al evento de rescate de civiles
        if (RescueManager.Instance != null)
        {
            RescueManager.Instance.OnCivilianRescued += UpdateCiviliansUI;
        }
    }

    private void OnDisable()
    {
        // Desuscripción obligatoria para evitar memory leaks al recargar escena
        PlayerHealth.OnHealthChanged -= UpdateHealthBar;
        PlayerHealth.OnPlayerDied -= ShowDefeatMenu;

        if (RescueManager.Instance != null)
        {
            RescueManager.Instance.OnCivilianRescued -= UpdateCiviliansUI;
        }
    }

    private void Start()
    {
        Time.timeScale = 1f; // Asegura que el juego arranque sin pausa si venís de reiniciar

        if (crosshairTexture != null)
        {
            Vector2 hotspot = new Vector2(crosshairTexture.width / 2f, crosshairTexture.height / 2f);
            Cursor.SetCursor(crosshairTexture, hotspot, CursorMode.Auto);
        }

        UpdateCiviliansUI(0);
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthFillImage != null && maxHealth > 0f)
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    private void ShowDefeatMenu()
    {
        if (defeatMenu != null)
        {
            defeatMenu.SetActive(true);
        }
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Devuelve el cursor normal para los botones
        Time.timeScale = 0f;
    }

    private void UpdateCiviliansUI(int rescuedCount)
    {
        if (civiliansText != null)
        {
            civiliansText.text = $"Civiles: {rescuedCount} / {totalCivilians}";
        }
    }
}