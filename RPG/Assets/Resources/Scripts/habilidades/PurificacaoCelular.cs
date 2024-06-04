using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurificacaoCelular : MonoBehaviour
{
    public int power = 1; // Poder de dano da habilidade
    public GameObject attack; // Referência ao GameObject que representa o ataque
    public float timerAttack; // Tempo de duração do ataque
    public float powerValue = 50; // Valor de intensidade do efeito de shake
    public float duration = 0.5f; // Duração do efeito de shake
    private bool isAttackActive = false; // Estado de atividade do ataque
    private bool isReactiveAttack = true;
    private float timer = 1f; // Contador de tempo
    public float radius; // Raio de efeito do ataque

    public float energiaUsada;
    private List<damageable> enemiesInRange = new List<damageable>(); // Lista de inimigos no alcance
    public CircleCollider2D circleCollider2D; // Referência ao CircleCollider2D para definir o raio
    public Animator anim; // Referência ao componente Animator

    public float reactivationDelay = 5f; // Tempo de reativação após o ataque desativar
    private float reactivationTimer = 0f; // Temporizador para reativação

    private bool isReactivation = false;
    public bool transformacaoAtivada;
    private Shaker shaker;

    private void Start()
    {
     
        shaker = GetComponent<Shaker>();
        // Inicializa o Animator
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator component not found on this object.");
        }

        // Verifica se o GameObject attack está atribuído
        if (attack == null)
        {
            Debug.LogError("The attack GameObject is not assigned in the Inspector.");
        }
        else
        {
            attack.SetActive(false); // Desativa o GameObject attack no início
        }

        // Inicializa o CircleCollider2D
        circleCollider2D = GetComponent<CircleCollider2D>();
        if (circleCollider2D == null)
        {
            Debug.LogError("CircleCollider2D component not found on this object.");
        }

        timer = timerAttack;
        reactivationTimer = reactivationDelay; // Inicializa o temporizador de reativação
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        damageable damageable = other.GetComponent<damageable>();

        if (damageable != null && attack != null && isReactivation == false)
        {
            AtivarAtaque(true);
        }
    }

    public void AtivarAtaque(bool state)
    {
        if (attack != null)
        {
            attack.SetActive(state);
            isAttackActive = state;

            // Encontra inimigos no alcance e aplica dano inicial
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enime") && isAttackActive)
                {
                    damageable damageable = enemy.GetComponent<damageable>();
                    if (damageable != null && !enemiesInRange.Contains(damageable) && isAttackActive)
                    {
                        enemiesInRange.Add(damageable);
                        damageable.TakeDamage(power, transform.position.x);
                        if(shaker != null){

                            Shaker.instance.SetValues(powerValue, duration);
                        }
                    }
                    else
                    {
                        Debug.LogWarning("damageable component is missing on enemy: " + enemy.name);
                    }
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        damageable damageable = other.GetComponent<damageable>();
        if (damageable != null && !enemiesInRange.Contains(damageable) && isAttackActive)
        {
            enemiesInRange.Add(damageable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       removerEnime(other);
    }

    private void Update()
    {

        // Atualiza o raio do CircleCollider2D se não for nulo
        if (circleCollider2D != null)
        {
            circleCollider2D.radius = radius;
        }

        // Atualiza o tamanho do ataque se estiver ativo
        if (attack != null && attack.activeSelf)
        {
            attack.transform.localScale = new Vector3(radius, radius, 1);
        }

        // Verifica se o ataque está ativo
        if (isAttackActive)
        {
            Debug.LogWarning("Contagem iniciada");
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isAttackActive = false;
                AtivarAtaque(false); // Desativa o ataque quando o tempo expirar
            }

            // Aplica dano contínuo aos inimigos dentro do raio
            foreach (damageable enemy in enemiesInRange)
            {
                if (isAttackActive && timer > 0f)
                {
                    enemy.TakeDamage(power, transform.position.x);
                     if(shaker != null){

                            Shaker.instance.SetValues(powerValue, duration);
                        }
                }
            }
            if(isAttackActive == false){
                RemoverTodosInimigos();
            }

           
        }else{ 
            Debug.LogWarning("Transformação não ativa, ataque não iniciado");
        }
     
    }

    public void ReactiveAttack(){
        if(attack != null){    
                
            if(isAttackActive == false && reactivationTimer > 0f){
                Debug.LogWarning("Contagem iniciada para reativar o ataque");
                isReactivation = true; 
                // Se o ataque está desativado, inicie o temporizador de reativação
                reactivationTimer -= Time.deltaTime;
                if (reactivationTimer <= 0f && timer < 1f)
                {
                    Debug.LogWarning("Contagem finalizada para reativar o ataque");
                    isReactiveAttack = true; // Torna o ataque reativável
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void SetTimer(float timerValue)
    {
        timer = timerValue;
    }

    public void SetReactiveTimerDelay(){
        reactivationTimer = reactivationDelay;
    }

    public float GetTimer()
    {
        return timer;
    }

    public bool GetIsAttackActive()
    {
        return isAttackActive;
    }

    public void SetIsAttackActive(bool state)
    {
        isAttackActive = state;
    }

   
    public float GetReactivationTimer(){
        return reactivationTimer;
    }

    public bool GetIsReactivation(){
        return isReactivation;
    }

    public void SetISReactivation(bool state){
        isReactivation = state;
    }

    void removerEnime(Collider2D other){
          damageable damageable = other.GetComponent<damageable>();
        if (damageable != null && enemiesInRange.Contains(damageable))
        {
            Debug.LogWarning("Inimigo removido");
            enemiesInRange.Remove(damageable);
        }
    }

     void RemoverTodosInimigos()
    {

        Debug.LogWarning("Todos os inimigos foram removidos");
        enemiesInRange.Clear();
    }

    public void AtivarReactiveAttack(bool state){
        isReactiveAttack = state;
    }

    public bool GetIsReactiveAttack(){
        return isReactiveAttack;
    }
}
