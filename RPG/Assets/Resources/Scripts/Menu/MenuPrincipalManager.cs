using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private LevelLoader levelLoader;

    // Variável estática para armazenar o nome da cena anterior
    public static string previousScene;

    // Método para jogar (transição para a cena do jogo)
    public void jogar()
    {
        levelLoader.Transition(nomeDoLevelDeJogo);
    }

    // Método para abrir o painel de opções
    public void abrirOpcoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    // Método para fechar o painel de opções
    public void fecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    // Método para sair do jogo
    public void sairJogo()
    {
        Debug.Log("Saindo do jogo");
        Application.Quit();
    }

    // Método para ir para a cena MenuInfo
    public void GoToMenuInfo()
    {
        previousScene = SceneManager.GetActiveScene().name; // Armazena a cena atual
        Debug.Log("Cena anterior armazenada: " + previousScene);
        SceneManager.LoadScene("MenuInfo");
    }

    // Método para retornar à cena anterior
    public void ReturnToPreviousScene()
    {
        Debug.Log("Tentando retornar para a cena: " + previousScene);
        if (!string.IsNullOrEmpty(previousScene))
        {
            SceneManager.LoadScene(previousScene);
        }
        else
        {
            Debug.LogWarning("Nenhuma cena anterior armazenada.");
        }
    }

    // Método para verificar e corrigir a configuração da cena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada: " + scene.name);
        var camera = Camera.main;
        if (camera != null)
        {
            Debug.Log("Câmera principal encontrada.");
        }
        else
        {
            Debug.LogError("Nenhuma câmera principal encontrada!");
        }

        // Verifique se há um Canvas na cena
        var canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            Debug.Log("Canvas encontrado.");
        }
        else
        {
            Debug.LogError("Nenhum Canvas encontrado!");
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}