using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Permite que a classe seja serializável e exibida no Unity Inspector
public class NPCQuest : MonoBehaviour
{
    // Referência para a quest associada ao NPC
    public Quest quest;
    // Referência para o script do personagem principal
    public Char MC;
    // Referência para o componente TextoQuest
    public TextoQuest texto;
    // Collider2D do NPCQuest
    private Collider2D colisor;

    // Método chamado quando o objeto é inicializado
    public void Start()
    {
        // Obtém a referência para o Collider2D do personagem principal
        colisor = MC.GetComponent<Collider2D>();
    }

    // Método para iniciar a quest associada ao NPC
    public void IniciarQuest()
    {
        // Define a quest como ativa
        quest.isActive = true;
        // Define a descrição da quest no componente TextoQuest
        texto.desc.text = quest.Descrição;
        // Define a quest associada ao personagem principal
        MC.quest = GetComponent<Quest>();
    }

    // Método chamado quando um objeto entra na área de colisão com o NPCQuest
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica se o objeto que entrou na área de colisão é o personagem principal e se ele já possui uma quest ativa
        if (collision == colisor && MC.quest != null)
        {
            // Se o personagem não possuir uma quest ativa, inicia a quest associada ao NPC
            if (!MC.quest.isActive)
            {
                IniciarQuest();
            }
        }
        // Se o personagem principal não possuir uma quest ativa e entrar na área de colisão com o NPCQuest, inicia a quest associada ao NPC
        else if (MC.quest == null && collision == colisor)
        {
            IniciarQuest();
        }
    }
}
