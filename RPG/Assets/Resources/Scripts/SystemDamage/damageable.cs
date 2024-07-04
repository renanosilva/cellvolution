using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class damageable : MonoBehaviour {

    // Variável para definir a saúde máxima
    public int maxHealth;

    // Tempo de invencibilidade após receber dano
    public float invincibleTime;

    // Eventos que serão invocados quando dano for recebido, liberado, ou quando ocorrer a morte
    public UnityEvent OnDamage;
    public UnityEvent ReleaseDamage;
    public UnityEvent OnDeath;

    // Variável para armazenar a saúde atual
    private int currentHealth;

    // Flags para controlar invencibilidade e estado de morte
    private bool invincible;
    private bool isDead;

    // Cor que o sprite vai assumir ao receber dano
    public Color damageColor;

    // Componente SpriteRenderer para alterar a cor do sprite
    private SpriteRenderer spriteRenderer;

    // Cor inicial do sprite
    private Color startColor;

    // Tempo de controle perdido após receber dano
    public float noControlTime = 0.1f;

    // Referência ao objeto de barra de vida
    [SerializeField]
    private BarrasController barraDeVida;
    [SerializeField]
    public BarrasController barraDeEnergia;

    [SerializeField]
    private Construir construir;

    public transformacao transformacao;

    private float energiaAtual = 100f;
    // Função chamada ao inicializar o script
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtém o SpriteRenderer
       

    }

    // Função chamada no início do jogo
   void Start()
{
    currentHealth = maxHealth; // Define a saúde atual como a máxima
    startColor = spriteRenderer.color; // Armazena a cor inicial do sprite

    if (barraDeVida != null)
    {
        this.barraDeVida.vidaMaxima = maxHealth; // Atualiza a barra de vida
        this.barraDeVida.vidaAtual = currentHealth;
    }
    else
    {
        Debug.LogWarning("Barra de Vida não atribuída no Inspector.");
    }

    if (barraDeEnergia != null)
    {
        barraDeEnergia.vidaMaxima = energiaAtual; // Atualiza a barra de vida
        barraDeEnergia.vidaAtual = energiaAtual;
    }
    else
    {
        Debug.LogWarning("Barra de Energia não atribuída no Inspector.");
    }
}


    void Update(){

        if(barraDeVida != null){
            barraDeVida.vidaAtual = currentHealth;
        }

        if (barraDeEnergia != null){

            energiaAtual = barraDeEnergia.vidaAtual;
        }
    }
    // Função para receber dano
    public void TakeDamage(int damageAmount, float xPos = 0)
    {
        if (invincible || isDead) // Se estiver invencível ou morto, não toma dano
            return;

        OnDamage.Invoke(); // Invoca o evento de dano
        invincible = true; // Torna o objeto invencível
        Invoke("SetInvincible", invincibleTime); // Define o tempo de invencibilidade
        Invoke("GainControl", noControlTime); // Define o tempo de perda de controle
        currentHealth -= damageAmount; // Diminui a saúde
        this.barraDeVida.vidaAtual = currentHealth; // Atualiza a barra de vida

        if (currentHealth <= 0) // Se a saúde chegar a 0
        {
            isDead = true; // Marca como morto
            OnDeath.Invoke(); // Invoca o evento de morte
        }
    }

    // Função para recuperar o controle após dano
    void GainControl()
    {
        if (isDead) // Se estiver morto, não recupera o controle
            return;
        ReleaseDamage.Invoke(); // Invoca o evento de liberação de dano
    }

    // Função para remover a invencibilidade
    void SetInvincible()
    {
        invincible = false; // Torna o objeto vulnerável novamente
    }

    // Inicia a animação de mudança de cor ao receber dano
    public void StartDamageSprite()
    {
        StartCoroutine(DamageSprite()); // Inicia a corrotina
    }

    // Corrotina para alternar a cor do sprite ao receber dano
    IEnumerator DamageSprite()
    {
        float timer = 0;
        while (timer < invincibleTime)
        {
            spriteRenderer.color = damageColor; // Altera a cor para a cor de dano
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundo
            timer += 0.1f;
            spriteRenderer.color = startColor; // Volta à cor inicial
            yield return new WaitForSeconds(0.1f); // Espera 0.1 segundo
            timer += 0.1f;
        }

        spriteRenderer.color = startColor; // Garante que a cor inicial seja restaurada
    }

    // Função para redefinir o estado após morte
    public void Respawn()
    {
        isDead = false; // Marca como vivo
        currentHealth = maxHealth; // Redefine a saúde
    }

    // Função para definir a saúde com um valor específico
    public void SetHealth()
    {
        int amount =2;
        currentHealth = (currentHealth * amount); // Aumenta a saúde
        if (currentHealth >= maxHealth) // Garante que a saúde não ultrapasse o máximo
        {
            currentHealth = maxHealth;
        }

    }

    // Função para obter a saúde atual
    public float GetHealth(){
        return currentHealth;
    }

    // Função para destruir o objeto após um tempo
    public void DestroyObject(float time)
    {
        Destroy(gameObject, time); // Destroi o objeto após 'time' segundos
    }
    
    public float DimiuirEnergia(float amount)
    {
        transformacao.SetCurrentEnergy(transformacao.GetCurrentEnergy() - amount);
        return energiaAtual;
        
    }

    public void AumentarEnergiaButao(){
        if(energiaAtual < 100f){
            Debug.Log("Energia aumentada");
            if(construir != null){
                if(construir.itensSuficientes()){	
                    construir.GastarConstruir();    
                     transformacao.SetCurrentEnergy(transformacao.GetCurrentEnergy() + 5f);
                    if(transformacao.IsTransformed() != true)
                    {
                        transformacao.SetIsTransformed(false);
                    }
                }

                if(transformacao.GetCurrentEnergy() > 100f){
                    transformacao.SetCurrentEnergy(100f);
                }

            }else{
                Debug.LogWarning("GameObject construir nulo");
            }
            
        }else{
            Debug.LogWarning("Energia cheia");
        }
    }
}
