using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construção : MonoBehaviour
{
    private Char Char;
    private bool podeAbrir;
    public GameObject[] telaConstrução;
    private int nivelAtual; // Nível atual da construção

    [SerializeField]
    public float distance;
    public TextoQuest t;
    public string txt;

    private bool missaoConcluida = false; // Variável para indicar se a missão foi concluída

    private void Start()
    {
        Char = FindObjectOfType<Char>();
    }

    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance);

        // Verifica se o personagem está dentro do círculo e atende às condições
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == Char.gameObject && podeAbrir && !txt.Equals(""))
            {
                t.desc.text = txt;
            }

            if (collider.gameObject == Char.gameObject && Input.GetKeyDown(KeyCode.C)
                && telaConstrução[nivelAtual].activeSelf == false && podeAbrir && !GetMissao()) // Verifica se a missão não está concluída
            {
                telaConstrução[nivelAtual].SetActive(true);
                Char.DisableControls();
            }else if (collider.gameObject == Char.gameObject && Input.GetKeyDown(KeyCode.C)
                && telaConstrução[nivelAtual].activeSelf && podeAbrir && !GetMissao()) // Verifica se a missão não está concluída
            {
                telaConstrução[nivelAtual].SetActive(false);
                Char.DisableControls();
            }
        }
    }

    public void SetPodeAbrirTrue()
    {
        podeAbrir = true;
    }

    // Método para definir o estado da missão
    public void SetMissaoConcluida(bool concluida)
    {
        distance = 0;
        missaoConcluida = concluida;
        Char.EnableControls();
       
    }
    public void SetProximoNivelConstrucao(){
        distance = 1.5f;
        missaoConcluida = false; 
        podeAbrir = false; 
        nivelAtual++;
    }

    public bool GetMissao()
    {
        return missaoConcluida;
    }
}
