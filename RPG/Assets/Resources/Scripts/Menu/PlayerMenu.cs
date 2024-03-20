using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour {

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Defina o parâmetro 'Speed' no animator para controlar a transição entre animações
        animator.SetInteger("Speed", 1); // Defina como 1 para ativar a animação
    }
}
