using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static MenuPausa instance;

    public GameObject pausePanel;

    [Header("Ativação do menu de informação")]
    public GameObject ativandoMenuInfo;
    public GameObject desativandoMenupausa;

    [Header("Ativação do menu de pausa")]
    public GameObject ativandoMenuPausa;
    public GameObject desativandoMenuInfo;

    [Header("Ativação da tela com componentes")]
    public GameObject ativandoTelaComponentes;

    [Header("referência ao levelLoader")]
    public LevelLoader levelLoader;

    private bool paused;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void LoadScene()
    {
        Time.timeScale = 1;
        Invoke("Loading", 0.5f);
    }

    void Loading()
    {
        //SceneManager.LoadSceneAsync("Menu");
        levelLoader.Transition("Menu");
    }

    public void Pause()
    {

        if (!paused && Time.timeScale == 0)
        {
            return;
        }

        paused = !paused;

        pausePanel.SetActive(paused);

        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public bool IsPaused()
    {
        return paused;
    }

    public void Quit()
    {
        Debug.Log("Saindo do jogo");
        Application.Quit();
    }

    public void AtivarMenuInfo()
    {
        if (ativandoMenuInfo != null)
        {
            ativandoMenuInfo.SetActive(true); // Ativa o menu de informação
        }

        if (desativandoMenupausa != null)
        {
            desativandoMenupausa.SetActive(false); // Desativa o menu de pausa
        }
    }

    public void AtivarMenuPausa()
    {
        if (ativandoMenuPausa != null)
        {
            ativandoMenuPausa.SetActive(true); // Ativa o menu de informação
        }

        if (desativandoMenuInfo != null )
        {
            desativandoMenuInfo.SetActive(false); // Desativa o menu de pausa
        }
    }

    public void AtivarTelaComponentes()
    {
        if (ativandoTelaComponentes != null)
        {
            ativandoTelaComponentes.SetActive(true); // Ativa o menu de informação
        }

        if (desativandoMenuInfo != null)
        {
            desativandoMenuInfo.SetActive(false); // Desativa o menu de pausa
        }
    }

    public void AtivarMenuPausa2()
    {
        if (ativandoMenuPausa != null)
        {
            ativandoMenuPausa.SetActive(true); // Ativa o menu de informação
        }

        if (ativandoTelaComponentes != null)
        {
            ativandoTelaComponentes.SetActive(false); // Desativa o menu de pausa
        }
    }
}
