using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacaoFinal : MonoBehaviour
{


    // Referência para o Animator do objeto
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Método chamado quando ocorre uma colisão 2D
    private void OnTrigerEnter2d(Collider2D othen)
    { 
        if (othen.CompareTag("Player"))
        {
            // Inicia a animação "NPAentry" do Animator quando ocorre uma colisão
            animator.Play("animaçãoFinal");

        }
    }
}
