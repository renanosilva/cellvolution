using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Introducao : MonoBehaviour
{
    public string[] textos;
    int contador = 0;
    public Text texto;

    [Header("referência ao levelLoader")]
    public LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        texto.text = textos[0];
    }

    private void FixedUpdate()
    {
        ProximoTexto();
    }

    public void ProximoTexto()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(contador < textos.Length - 1)
            {
                contador++;
                texto.text = textos[contador];
            }
            else
            {
                levelLoader.Transition("Organismo");
                return;
            }
        }
    }
}
