using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Inventario Dinámico de Armas")]
    public List<WeaponBase> armasDisponibles = new List<WeaponBase>();
    private int indiceArmaActual = 0;

    [Header("Sprites del Jugador (en el mismo orden que las armas)")]
    public List<Sprite> spritesPorArma = new List<Sprite>();
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Si ya cargaste armas desde el Inspector al iniciar, equipamos la primera
        if (armasDisponibles.Count > 0)
        {
            EquiparArma(0);
        }
    }

    void Update()
    {
        // Si la lista está vacía, no hacemos nada para evitar errores
        if (armasDisponibles.Count == 0) return;

        // 1. Cambiar de arma con la rueda del mouse (Scroll)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            int siguienteIndice = (indiceArmaActual + 1) % armasDisponibles.Count;
            EquiparArma(siguienteIndice);
        }
        else if (scroll < 0f)
        {
            int anteriorIndice = (indiceArmaActual - 1 + armasDisponibles.Count) % armasDisponibles.Count;
            EquiparArma(anteriorIndice);
        }

        // 2. Cambiar de arma con teclas 1, 2, 3... según el tamaño actual de la lista
        if (Input.GetKeyDown(KeyCode.Alpha1) && armasDisponibles.Count >= 1) EquiparArma(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) && armasDisponibles.Count >= 2) EquiparArma(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) && armasDisponibles.Count >= 3) EquiparArma(2);

        // 3. Disparar el arma actualmente seleccionada de la lista
        WeaponBase armaEquipada = armasDisponibles[indiceArmaActual];

        if (armaEquipada.isAutomatic)
        {
            if (Input.GetMouseButton(0))
                armaEquipada.Shoot();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
                armaEquipada.Shoot();
        }
    }

    public void EquiparArma(int nuevoIndice)
    {
        // Validación de seguridad dentro de los límites de la Lista
        if (nuevoIndice < 0 || nuevoIndice >= armasDisponibles.Count) return;

        indiceArmaActual = nuevoIndice;

        // Activar solo el GameObject del arma equipada y desactivar las demás
        for (int i = 0; i < armasDisponibles.Count; i++)
        {
            armasDisponibles[i].gameObject.SetActive(i == indiceArmaActual);
        }

        // Actualizar el sprite del jugador si corresponde
        if (spriteRenderer != null && indiceArmaActual < spritesPorArma.Count)
        {
            spriteRenderer.sprite = spritesPorArma[indiceArmaActual];
        }
    }

    // Método extra para recoger armas nuevas durante la partida (demuestra el uso dinámico de List)
    public void AgregarArma(WeaponBase nuevaArma, Sprite nuevoSprite)
    {
        if (!armasDisponibles.Contains(nuevaArma))
        {
            armasDisponibles.Add(nuevaArma);
            spritesPorArma.Add(nuevoSprite);
            EquiparArma(armasDisponibles.Count - 1); // Equipa automáticamente la recién recogida
        }
    }
}