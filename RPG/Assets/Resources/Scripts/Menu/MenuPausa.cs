using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public static MenuPausa instance;

    public GameObject pausePanel;

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
}
