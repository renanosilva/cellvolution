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

    [Header("Nomes Animações")]
    public string animacaoPlay;
    public string animacaoExit;

    private void Update()
    {

        if (dialogueControl.turnoAtual == TurnoAnimPlay)
        {
            animator.Play(animacaoPlay);
        }

        if (dialogueControl.turnoAtual == turnoAnimExit)
        {
            animator.Play(animacaoExit);
        }
    }

}
