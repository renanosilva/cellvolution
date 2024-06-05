﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transformacao : MonoBehaviour
{
    public float energyCostPerSecond = 3f; // Quantidade de energia consumida por segundo
    public float cooldownTime = 20f; // Tempo de cooldown em segundos

    private float currentEnergy= 100f; // Energia atual do personagem
    private float cooldownTimer; // Temporizador para o cooldown
    private bool isTransformed = false; // Indica se a transformação está ativa
    private bool isInCooldown = false; // Indica se a habilidade está em cooldown
    private bool DisableTransform = false;
    public bool transformBloque = false;
    public Char personagem;
    private Animator anim; 

    [SerializeField]
    public BarrasController barraDeEnergia;
    public GameObject canvas;
    public GameObject canvaTimer;  
    public BarrasController barraDeTempo;
    public BarrasController energiaBarra;
    public PurificacaoCelular purificacaoCelular;
    public damageable damageable;


    void Start()
    {
        cooldownTimer = cooldownTime;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(transformBloque){
            // Gerencia a transformação
            if (isTransformed)
            {
                DisableTransform = true;

                if(Input.GetKey(KeyCode.Q) && purificacaoCelular.GetIsReactiveAttack() == true  && GetCurrentEnergy() >= 15f && IsTransformed() == true){
                    anim.SetTrigger("OnPurificacao");

                    purificacaoCelular.SetISReactivation(false);
                    purificacaoCelular.AtivarReactiveAttack(false);
                    purificacaoCelular.AtivarAtaque(true);
                    purificacaoCelular.SetTimer(purificacaoCelular.timerAttack);
                    purificacaoCelular.SetReactiveTimerDelay();
                    purificacaoCelular.radius = 2f;

                    anim.SetTrigger("OnPurificacao2");

                    canvaTimer.SetActive(true);

                    barraDeTempo.vidaAtual = purificacaoCelular.timerAttack;
                    barraDeTempo.vidaMaxima = purificacaoCelular.timerAttack;
                    energiaBarra.vidaAtual = damageable.DimiuirEnergia(purificacaoCelular.energiaUsada);

                    Debug.LogWarning("Ativando purificacao");
                }else if(purificacaoCelular.GetIsAttackActive() == false && purificacaoCelular.GetTimer() <= 0f){

                    Debug.LogWarning("Desativando purificacao");
                    anim.SetTrigger("DesativarPurificacao");
                    purificacaoCelular.AtivarAtaque(false);
                    purificacaoCelular.SetTimer(0.8f);

                }

                if(purificacaoCelular.GetIsReactiveAttack() == false && purificacaoCelular.GetIsAttackActive() == false && purificacaoCelular.GetTimer() <= 1f){

                    Debug.LogWarning("Reativando purificacao");
                    purificacaoCelular.ReactiveAttack();
                }

                if(purificacaoCelular.GetReactivationTimer() < 0f){

                    canvaTimer.SetActive(false);
                }

                canvas.gameObject.SetActive(true);
                currentEnergy -= energyCostPerSecond * Time.deltaTime;
                
                isInCooldown = false;
                // Diminui a energia enquanto a transformação está ativa

                barraDeEnergia.vidaAtual = currentEnergy;

                // Verifica se a energia acabou
                if (currentEnergy <= 0)
                {
                    // Desativa a transformação
                    DeactivateTransformation();
                    currentEnergy = 0;	
                }
            }

            if(isTransformed == false && DisableTransform == true){

                DeactivateTransformation();
                DisableTransform = false;
                Debug.LogWarning("Desativando transformacao");
            }

            // Gerencia o cooldown
            if (isInCooldown)
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0)
                {
                    isInCooldown = false;
                }
            }
            
        }
    
    }

    // Função para ativar a transformação
    public void ActivateTransformation(bool ativa)
    {
        if (isTransformed == false && !isInCooldown && currentEnergy > 0)
        {
            isTransformed = ativa;
        }
    }

    // Função para desativar a transformação
    public void DeactivateTransformation()
    {
        if (isTransformed == false)
        {
            EndTransformation();
            Debug.LogWarning("Desativando transformacao chamou");
        }
    }

    // Função privada para finalizar a transformação
    private void EndTransformation()
    {
        isTransformed = false;
        isInCooldown = true;
        cooldownTimer = cooldownTime;
        anim.SetTrigger("OffTransformacao");
    }

    // Função para definir a energia atual (pode ser chamada de outro script)
    public void SetCurrentEnergy(float energy)
    {
        currentEnergy = energy;
    }

    // Função para obter a energia atual
    public float GetCurrentEnergy()
    {
        return currentEnergy;
    }

    public void SetIsTransformed(bool state)
    {
        isTransformed = state;
        isInCooldown= false;
        ActivateTransformation(isTransformed);
    }

    // Função para verificar se a transformação está ativa
    public bool IsTransformed()
    {
        return isTransformed;
    }

    // Função para verificar se está em cooldown
    public bool IsInCooldown()
    {
        return isInCooldown;
    }
}