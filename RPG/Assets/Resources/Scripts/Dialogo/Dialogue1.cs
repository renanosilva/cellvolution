using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue1 : MonoBehaviour
{
    public Sprite profile;
    public string[] speechText;
    public string actorName;
    private bool estaAberto;

    public NPC npc;
    public NPC1 npc1;
    public NPC2 npc2;
    public Char player;

    public LayerMask playerLayer;
    public float radius;

    public DialogueControl dc;
    bool onRadious;

    private void Start()
    {
        dc = FindObjectOfType<DialogueControl>();
        
    }

    private void Update()
    {
        if (npc.condição == true )
        {
            speechText = npc.falas;
            dc.qtdTurnos = npc.qtdTurnosnpc;
        }
        else if (npc1.condição == true)
        {
            speechText = npc1.falas;
            dc.qtdTurnos = npc1.qtdTurnosnpc;
        }

        else if (npc2.condição == true)	
        {
            speechText = npc2.falas;
            dc.qtdTurnos = npc2.qtdTurnosnpc;
        }



        if (/*Input.GetKeyDown(KeyCode.Space) &&*/ onRadious && !estaAberto)
        {
            dc.Speech(profile, speechText, actorName);
            estaAberto = true;
            player.DisableControls();
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
}
