using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] // Permite que a classe seja serializável e exibida no Unity Inspector
public class NPC : MonoBehaviour
{
    [Header("EnemyConfig")] // Cabeçalho para configurações do NPC
    
    // Nome do NPC
    public string nome;
    // Condição que determina se o diálogo pode ser iniciado
    public bool condição = false;
    // Referência para o script do personagem
    private Char @char;

    [Header("Imports")] // Cabeçalho para importações de outros componentes
    // Referência para o script do diálogo
    // Lista de falas do NPC
    public string[] falas;
    public int[] qtdTurnosnpc;
    // Referência para o Collider2D
    private Collider2D colisor;
    // Referência para a quest associada ao NPC
    private Quest quest;

    [Header("No Fim Das Falas")] // Cabeçalho para eventos ao final das falas
    // Evento Unity que é invocado ao final do diálogo
    public UnityEvent OnDialogueEnd;

    public Dialogue1 dialogue;

    // Método chamado quando o objeto é inicializado
    private void Start()
    {
        // Obtém a referência para o Collider2D do personagem
        colisor = GameObject.Find("MC").GetComponent<Collider2D>();
        // Obtém a referência para o script do personagem
        @char = GameObject.Find("MC").GetComponent<Char>();
        // Obtém a referência para a quest associada ao NPC
        quest = GetComponent<Quest>();
    }

    // Método chamado quando o personagem entra na área de interação com o NPC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na área de colisão é o personagem
        if (colisor.tag == "Player")
        {
            // Define o NPC como o NPC do diálogo
        }
        // Verifica se a condição para iniciar o diálogo é verdadeira
        if (condição == true)
        {
            // Inicia o diálogo
        }
    }

    // Método chamado quando o personagem sai da área de interação com o NPC
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verifica se o objeto que saiu da área de colisão é o personagem e se a condição para iniciar o diálogo é verdadeira
        if (colisor.tag == "Player" && condição == true)
        {

            if(dialogue.forcaMembranaRequerida <= AtributoManager.instance.forcaMembrana || dialogue.nivelComunicacaoRequerido <= AtributoManager.instance.nivelComunicacao)
            {
                // Invoca o evento ao final do diálogo
                OnDialogueEnd.Invoke();
            }
        }
    }

    // Método para definir a condição como verdadeira
    public void setCondicaoTrue()
    {
        condição = true;
    }

    // Método para definir a condição como falsa
    public void setCondicaoFalse()
    {
        condição = false;
    }

    // Método para destruir o NPC
    public void destroySelf()
    {
        Destroy(this);
    }
}
