using System.Collections;
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
    public bool transformBloque = false;
    public Char personagem;
    private Animator anim; 

    [SerializeField]
    public BarrasController barraDeEnergia;
    public GameObject canvas;

    public PurificacaoCelular purificacaoCelular;

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
                canvas.gameObject.SetActive(true);

                isInCooldown = false;
                // Diminui a energia enquanto a transformação está ativa
                currentEnergy -= energyCostPerSecond * Time.deltaTime;

                barraDeEnergia.vidaAtual = currentEnergy;
                // Verifica se a energia acabou
                if (currentEnergy <= 0)
                {
                    // Desativa a transformação
                    DeactivateTransformation();
                    currentEnergy = 0;	
                }
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

            if(isTransformed == false && isInCooldown ==false){
                Debug.LogWarning("Transformação concluída! Reinicie o jogo para recomeçar a transformação.");
            }else{

            }
            
        }
    
    }

    // Função para ativar a transformação
    public void ActivateTransformation()
    {
        if (!isTransformed && !isInCooldown && currentEnergy > 0)
        {
            isTransformed = true;
        }
    }

    // Função para desativar a transformação
    public void DeactivateTransformation()
    {
        if (isTransformed == false)
        {
            EndTransformation();
        }
    }

    // Função privada para finalizar a transformação
    private void EndTransformation()
    {
        isTransformed = false;
        isInCooldown = true;
        cooldownTimer = cooldownTime;
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
