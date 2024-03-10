using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construção : MonoBehaviour
{
    // Referência para o script do personagem
    private Char Char;
    // Flag para indicar se pode abrir a tela de construção
    private bool podeAbrir;
    // Referência para o objeto da tela de construção
    public GameObject telaConstrução;

    // Distância para ativar a interação com a construção
    [SerializeField]
    public float distance;

    private void Start()
    {
        // Encontra e atribui a referência para o script do personagem
        Char = FindObjectOfType<Char>();
    }

    void Update()
    {
        // Verifica se o personagem está próximo e pressionou a tecla E para interagir com a construção
        if (Vector2.Distance(Char.transform.position, transform.position) < distance && Input.GetKeyDown(KeyCode.E) && !telaConstrução.activeSelf && podeAbrir)
        {
            // Ativa a tela de construção e desabilita os controles do personagem
            telaConstrução.SetActive(true);
            Char.DisableControls();
        }
    }

    // Método para definir a flag que permite abrir a tela de construção
    public void SetPodeAbrirTrue()
    {
        podeAbrir = true;
    }
}
