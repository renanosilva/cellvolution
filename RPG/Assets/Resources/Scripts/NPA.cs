using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPA : MonoBehaviour
{
    // Referência para o Collider2D do personagem
    private Collider2D collider;
    // Referência para o Animator do objeto
    public Animator animator;

    // Método chamado quando o objeto é inicializado
    private void Start()
    {
        // Obtém a referência para o Collider2D do personagem
        collider = GameObject.Find("MC").GetComponent<Collider2D>();
    }

    // Método chamado quando ocorre uma colisão 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Inicia a animação "NPAentry" do Animator quando ocorre uma colisão
        animator.Play("NPAentry");
    }
}
