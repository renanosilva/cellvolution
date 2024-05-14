using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private LevelLoader levelLoader;

    public void jogar()
    {
        levelLoader.Transition(nomeDoLevelDeJogo);
    }
    public void abrirOpcoes() {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }
    public void fecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }
    public void sairJogo()
    {
        Debug.Log("Saimento do jogo");
        Application.Quit();
    }
}
