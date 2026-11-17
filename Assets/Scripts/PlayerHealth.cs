using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{

    public static event Action<float, float> OnHealthChanged;
    public static event Action OnPlayerDied;

    [SerializeField] private float playerMaxHealth = 100f;
    [SerializeField] private GameObject defeatMenu;
    private float playerCurrentHealth;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        playerCurrentHealth = playerMaxHealth;
        OnHealthChanged?.Invoke(playerCurrentHealth, playerMaxHealth);
    }


    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            Die();
        }

       
    }


    public virtual void TakeDamage(float damage)
    {
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth - damage, 0, playerMaxHealth);
        OnHealthChanged?.Invoke(playerCurrentHealth, playerMaxHealth);

        if (playerCurrentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
        }
    }

    private void Die()
    {
        // Aquí puedes agregar la lógica para manejar la muerte del jugador, como reiniciar el nivel o mostrar un menú de derrota.
        Debug.Log("El jugador ha muerto.");
        defeatMenu.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
    {
        // Si tenés un script de movimiento del jugador, podés desactivarlo brevemente acá
        rb.linearVelocity = direction.normalized * force; 
        yield return new WaitForSeconds(duration);
        rb.linearVelocity = Vector2.zero;
    }

}
