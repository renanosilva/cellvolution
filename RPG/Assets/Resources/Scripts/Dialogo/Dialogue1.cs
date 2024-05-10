using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue1 : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;
    public bool estaAberto;
    public bool celulaMissao;

    [Header("Referência ao NPC")]
    public NPC npc;
    public NPC1 npc1;
    public NPC2 npc2;
    [Header("Referência ao player")]
    public Char player;

    [Header("Habilidades requeridas")]
    public int forcaMembranaRequerida;
    public int nivelComunicacaoRequerido;
    public string textoBloqueio;

    [Header("Referência ao grupo das missões secundárias")]
    public GameObject grupoMissoes;

    [Header("Layer do player")]
    public LayerMask playerLayer;
    [Header("Raio de colisão")]
    public float radius;

    [Header("Referência ao dialogue control")]
    public DialogueControl dc;
    public bool onRadious;


    private void Update()
    {
        if (npc.condição == true )
        {
            speechText = npc.falas;
            dc.qtdTurnos = npc.qtdTurnosnpc;
        }

        if (npc1.condição == true)
        {
            speechText = npc1.falas;
            dc.qtdTurnos = npc1.qtdTurnosnpc;
        }

        if (npc2 != null && npc2.condição == true)	
        {
            speechText = npc2.falas;
            dc.qtdTurnos = npc2.qtdTurnosnpc;
        }



        if (/*Input.GetKeyDown(KeyCode.Space) &&*/ onRadious && !estaAberto)
        {

            if ((AtributoManager.instance.forcaMembrana >= forcaMembranaRequerida) && (AtributoManager.instance.nivelComunicacao >= nivelComunicacaoRequerido))
            {
                dc.Speech(profile, speechText, actorName);
                estaAberto = true;
                player.DisableControls();

                SetGrupoMissoes(false);
                
            }
            else
            {
                string[] speechTextISF = new string[1];
                int[] qtdTurnosISF = new int[1];

                speechTextISF[0] = textoBloqueio;
                qtdTurnosISF[0] = 1;

                dc.qtdTurnos = qtdTurnosISF;

                dc.Speech(profile, speechTextISF, "Bloqueado");
                estaAberto = true;
                player.DisableControls();

                SetGrupoMissoes(true);

            }
        }
        else if(!onRadious && celulaMissao)
        {
            estaAberto = false;
        }

    }

    private void FixedUpdate()
    {
        Interact();

        
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if(hit != null)
        {
            onRadious = true;
        }
        else
        {
            onRadious = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void SetEstaAberto(bool state)
    {
        estaAberto = state;
    }

    public void SetGrupoMissoes(bool state)
    {
        if(grupoMissoes != null)
        {
            grupoMissoes.SetActive(state);
        }
    }
}
