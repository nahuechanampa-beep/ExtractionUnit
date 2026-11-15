using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;

    public void PlayGame()
    { 
        Debug.Log("Iniciando el juego...");
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }


}
