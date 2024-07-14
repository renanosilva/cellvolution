using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private LevelLoader levelLoader;

    // Método para jogar (transição para a cena do jogo)
    public void jogar()
    {

        if(CheckpointManager.instance.textoMissaoAtual == "")
        {
            levelLoader.Transition(nomeDoLevelDeJogo);
        }
        else
        {
            levelLoader.Transition("Organismo");
        }

    }

    public void NovoJogo()
    {
        CheckpointManager.instance.Reset();
        AtributoManager.instance.Reset();
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
}
