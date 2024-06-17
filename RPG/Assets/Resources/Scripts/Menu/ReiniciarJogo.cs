using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarJogo : MonoBehaviour
{
    // Método para iniciar um novo jogo
    public void NovoJogo()
    {
        // Carregar a cena do jogo
        SceneManager.LoadScene("Organismo");
    }
}
