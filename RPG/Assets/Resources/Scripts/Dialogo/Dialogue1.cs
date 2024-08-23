using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Text textoMissoes;

    [Header("Referência ao player")]
    public Char player;
    private Collider2D colisor;

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

    [Header("Atributo referente ao Checkpoint")]
    public int indiceCheckpoint;

    private GameObject seta;
    

    private void Start()
    {
        colisor = GameObject.Find("MC").GetComponent<Collider2D>();
        textoMissoes = GameObject.Find("TextoMissão").GetComponent<Text>();
        seta = GameObject.Find("TargetIndicator");
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (colisor == collision)
        {
            VerificarNPC();
            ChamarDialogo();
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colisor == collision && celulaMissao == true)
        {
            estaAberto = false;
        }

        if((colisor == collision) && ((AtributoManager.instance.forcaMembrana < forcaMembranaRequerida) || (AtributoManager.instance.nivelComunicacao < nivelComunicacaoRequerido)))
        {
            textoMissoes.text = "Procure outras células e realize as missões secundárias até obter os níveis de atributos necessários";
            AudioManager som = GameObject.Find("MC").GetComponent<AudioManager>();
            som.PlayAudio(Resources.Load<AudioClip>("audios/Quest Completa"));
        }
    }


    public void VerificarNPC()
    {
        if (npc != null && npc.condição == true)
        {   
            npc.enabled = true;
            speechText = npc.falas;
            dc.qtdTurnos = npc.qtdTurnosnpc;
        }

         if (npc1 != null && npc1.condição == true)
        {
            npc1.enabled = true;
            speechText = npc1.falas;
            dc.qtdTurnos = npc1.qtdTurnosnpc;
        }

         if (npc2 != null && npc2.condição == true)
        {
            npc2.enabled = true;
            speechText = npc2.falas;
            dc.qtdTurnos = npc2.qtdTurnosnpc;
        }

    }

    public void ChamarDialogo()
    {
        if (!estaAberto)
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

            if(seta != null)
            {
                seta.SetActive(false);
            }
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
