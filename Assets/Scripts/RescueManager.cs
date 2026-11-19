using System;
using UnityEngine;

public class RescueManager : MonoBehaviour
{
    public static RescueManager Instance { get; private set; }

    public int RescuedCivilians { get; private set; } = 0;

    // Evento (Patrón Observer) para avisar al HUD o paneles cuando cambia el número
    public event Action<int> OnCivilianRescued;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void RegisterRescue()
    {
        RescuedCivilians++;
        OnCivilianRescued?.Invoke(RescuedCivilians);
    }
}
