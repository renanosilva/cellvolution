using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construção : MonoBehaviour
{
    private Char Char;
    private bool podeAbrir;
    public GameObject telaConstrução;

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
        if (Vector2.Distance(Char.transform.position, transform.position) < distance && podeAbrir)
        {
            t.desc.text = txt;
        }
        if (Vector2.Distance(Char.transform.position, transform.position) < distance
            && Input.GetKeyDown(KeyCode.C)
            && !telaConstrução.activeSelf
            && podeAbrir
            && !GetMissao()) // Verifica se a missão não está concluída
        {
            
            telaConstrução.SetActive(true);
            Char.DisableControls();
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
    }
    public bool GetMissao()
    {
        return missaoConcluida;
    }
}
