using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LegendaControl : MonoBehaviour {

    private bool onRadious;
    public float radius;
    public LayerMask playerLayer;
    public TextMeshPro Objetolegenda;

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (onRadious)
        {
            animator.SetTrigger("Start");
        }
        else
        {
            animator.SetTrigger("Exit");
        }
    }

    private void FixedUpdate()
    {
        Interact();
    }

    public void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, playerLayer);

        if (hit != null)
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

    public void SetCorLegenda()
    {
        Objetolegenda.color = new Color(0, 1, 0, 1);
    }

    
}
