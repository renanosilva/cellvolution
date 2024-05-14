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
        StartCoroutine(rotina());
    }

    public IEnumerator rotina()
    {
        texto.text = textos[contador];
        contador++;

        if(contador < textos.Length) {
            yield return new WaitForSeconds(5);
            StartCoroutine(rotina());
        } else {
            levelLoader.Transition("Organismo");
        }

    }
}
