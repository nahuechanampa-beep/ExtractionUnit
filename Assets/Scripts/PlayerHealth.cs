using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerMaxHealth = 100;
    [SerializeField] private GameObject defeatMenu;
    private int playerCurrentHealth;
    private float enemyDamage { get { return 10f; } } // Daño que inflige el enemigo al jugador
    private void Awake()
    {
        playerCurrentHealth = playerMaxHealth;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            Die();
        }

       
    }


    private void TakeDamage(int damage)
    {
        playerCurrentHealth -= damage;
        if (playerCurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Aquí puedes agregar la lógica para manejar la muerte del jugador, como reiniciar el nivel o mostrar un menú de derrota.
        Debug.Log("El jugador ha muerto.");
        defeatMenu.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego
    }



}
