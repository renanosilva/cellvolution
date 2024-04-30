using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimation : MonoBehaviour
{
    [Header("Classes")]
    public DialogueControl dialogueControl;
    public Animator animator;

    [Header("Turnos Animações")]
    public int TurnoAnimPlay;
    public int turnoAnimExit;

    //define o npc onde será aplicada a animação, 0 para npc, 1 para npc1, 2 para npc2
    public int npcAnimacao;

    [Header("Nomes Animações")]
    public string animacaoPlay;
    public string animacaoExit;

    [Header("NPC")]
    public NPC npc;
    public NPC1 npc1;
    public NPC2 npc2;
    private int npcAtual;

    private void Update()
    {

        VerificarNpcAtual();

        if (dialogueControl.turnoAtual == TurnoAnimPlay && npcAtual == npcAnimacao)
        {
            animator.Play(animacaoPlay);
        }

        if (dialogueControl.turnoAtual == turnoAnimExit && npcAtual == npcAnimacao)
        {
            animator.Play(animacaoExit);
        }
    }

    private void VerificarNpcAtual()
    {
        if(npc.condição == true) {
            npcAtual = 0;
        } else if(npc1.condição == true) {
            npcAtual = 1;
        } else if(npc2.condição == true) {
            npcAtual = 2;
        }
    }

}
