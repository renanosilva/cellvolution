using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable] // Permite que a classe seja serializável e exibida no Unity Inspector
public class Quest : MonoBehaviour
{
    // Título da quest
    public string titulo;
    // Descrição da quest
    public string Descrição;
    // Referência para o NPCQuest associado à quest
    public NPCQuest nPC;
    // Referência para o componente TextoQuest
    public TextoQuest textoQuest;
    // Referência para o NPC relacionado à quest
    public NPC npc;
    // Evento Unity invocado ao completar a quest
    public UnityEvent OnQuestEnd;
    // Objeto que define o objetivo da quest
    public QuestObjetivo objetivo;
    // Indica se a quest está ativa
    public bool isActive;
    
    // Método para remover uma descrição específica da descrição da quest
    public void RemoverDesc(string removivel)
    {
        // Remove a substring especificada da descrição e ajusta o texto
        textoQuest.desc.text = textoQuest.desc.text.Replace(removivel, "").TrimEnd();
    }
    
    // Método chamado quando a quest é completada
    public void Completo()
    {
        // Remove a descrição da quest
        RemoverDesc(Descrição);
        // Define a quest como não ativa
        isActive = false;
        // Invoca o evento de fim da quest
        OnQuestEnd.Invoke();
        // Destroi o NPCQuest associado à quest
        Destroy(nPC);
        // Destroi a própria quest
        Destroy(this);
        // Exibe uma mensagem no console indicando que a quest foi completada
        Debug.Log(titulo + " completo");
    }
}
