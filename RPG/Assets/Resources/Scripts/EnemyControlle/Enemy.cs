using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Saúde do inimigo
    public float health;
    // Referência ao objeto do jogador
    public GameObject Player; 
    // Velocidade de movimento do inimigo
    public float speed;
    // Distância mínima para o jogador que ativa certos comportamentos
    private float distancePlayer = 10f;

    // Ponto inicial do inimigo
    public Transform startPoint;

    // Referência ao componente Rigidbody2D do inimigo
    private Rigidbody2D rb;
    // Referência ao componente CircleCollider2D do inimigo
    private CircleCollider2D circle;
    
    // Referência ao componente SpriteRenderer do inimigo
    private SpriteRenderer sprite;

    // Referência ao componente damageable do inimigo
    private damageable damageable;

    // Referência ao script PurificacaoCelular no jogador
    public PurificacaoCelular purificacao;

    // Taxa de disparo do inimigo
    public float fireRate;
    // Tempo do próximo disparo permitido
    private float nextFireTime;

    // Array de pontos de passagem para onde a plataforma deve se mover
    public Transform[] wayPoints;
    // Tempo de espera entre mudanças de direção
    public float waitTime;
    // Direção do movimento: 1 para frente, -1 para trás
    private int dir = 1;
    // Índice atual no array de wayPoints
    private int index;
    // Flag para verificar se a plataforma está esperando
    private bool wait;
    // Temporizador para contar o tempo de espera
    private float timer;

    // Referência ao componente Animator do inimigo
    private Animator anim;

    private bool startMove = true;

    // Lista para rastrear clones do enemyPrefab
    private List<Rigidbody2D> enemyClones = new List<Rigidbody2D>();

    public GameObject dialogue;

    // Construtor da classe Enemy
    public Enemy() {}

    // Use this for initialization
    void Start () {
        // Obtém o componente Rigidbody2D anexado ao GameObject
        rb = GetComponent<Rigidbody2D>();
        // Obtém o componente SpriteRenderer anexado ao GameObject
        sprite = GetComponent<SpriteRenderer>();
        // Obtém o componente damageable anexado ao GameObject
        damageable = GetComponent<damageable>();

        // Posiciona o inimigo no ponto inicial
        transform.position = startPoint.position;   
        // Inicializa o próximo tempo de disparo para o tempo atual
        nextFireTime = Time.time;
        
    }

   

    // Chamado em intervalos fixos para atualizações de física
    void FixedUpdate(){  

        if(dialogue.gameObject.activeSelf == true || AtributoManager.instance.bloquearTela == true)
        {
            startMove = false;
        }else if(dialogue.gameObject.activeSelf == false && AtributoManager.instance.bloquearTela == false)
        {
            startMove = true;
        }
        // Obtém a saúde atual do inimigo
        health = damageable.GetHealth();
        float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);
        // Chama o método Move para atualizar o movimento do inimigo
        if(distancePlayer > distanceToPlayer){	
            Move();
        }
        if (distancePlayer < distanceToPlayer)
        {
            // Se estiver em espera, conta o tempo de espera e retorna
            if (wait)
            {
                CoutingWaitTime();
                return;
            }

            // Verifica se precisa mudar os waypoints
            ChangeWaypoints();

            // Move a plataforma
            Moving();
        }
       
    }
    
    // Método para mover o inimigo
    void Move(){
        if (health > 0 && startMove) {
            // Calcula a distância entre o jogador e o inimigo
            float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);
            
            // Verifica se a distância ao jogador é menor que a distância definida e se o ataque do jogador está ativo
            if (distanceToPlayer < distancePlayer && purificacao.GetIsAttackActive()) {
                Flee(); // Chama o método Flee para fugir
            } else if (distanceToPlayer < distancePlayer) {
                FollowPlayer(); // Chama o método FollowPlayer para seguir o jogador
            } else {
                ReturnToStartPoint(); // Chama o método ReturnToStartPoint para retornar ao ponto inicial
            }
        } else if(health <= 0) {
            EnemyDeath(); // Chama o método EnemyDeath se a saúde for 0 ou menor
        }
    }


    // Método para seguir o jogador
    void FollowPlayer()
    {
        // Move o inimigo em direção ao jogador
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

        // Inverte o sprite dependendo da posição do jogador
        sprite.flipX = transform.position.x > Player.transform.position.x;

    }

    // Método para retornar ao ponto inicial
    void ReturnToStartPoint()
    {
        // Move o inimigo de volta ao ponto inicial
        transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);

        // Inverte o sprite dependendo da posição do ponto inicial
        sprite.flipX = transform.position.x > startPoint.position.x;
    }

    // Método para fugir do jogador
    void Flee()
    {
        // Calcula a direção oposta ao jogador
        Vector3 directionAwayFromPlayer = transform.position - Player.transform.position;
        // Calcula a nova posição de fuga
        Vector3 fleePosition = transform.position + directionAwayFromPlayer.normalized * speed * Time.deltaTime;

        // Move o inimigo para a posição de fuga
        transform.position = fleePosition;

        // Inverte o sprite dependendo da posição do jogador
        sprite.flipX = transform.position.x > Player.transform.position.x;
    }

    // Método para tratar a morte do inimigo
    void EnemyDeath() {
        health = 0;
        speed = 0;

        // Destruir todos os clones
        foreach (var clone in enemyClones) {
            if (clone != null) {
                Destroy(clone.gameObject);
            }
        }
        // Limpar a lista após destruir os clones
        enemyClones.Clear();

        // Destruir os componentes do inimigo e o próprio GameObject
        Destroy(transform.gameObject.GetComponent<CircleCollider2D>());
        Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1f); 
    }

    // Método para contar o tempo de espera
    void CoutingWaitTime()
    {
        // Incrementa o temporizador com o tempo desde o último frame
        timer += Time.deltaTime;

        // Se o temporizador atingir o tempo de espera, reseta a espera e o temporizador
        if (timer >= waitTime)
        {
            wait = false;
            timer = 0;
        }
    }

    // Método para mudar os waypoints quando necessário
 void ChangeWaypoints() {
    if (index < 0 || index >= wayPoints.Length) {
        return; // Saia da função se o índice estiver fora dos limites
    }

    // Calcula a distância até o próximo waypoint
    float distance = Vector2.Distance(transform.position, wayPoints[index].position);

    // Se a distância for zero ou menor, escolha um novo waypoint aleatório
    if (distance <= 0) {
        int newIndex = Random.Range(0, wayPoints.Length);
        
        // Certifique-se de que o novo índice é diferente do atual
        while (newIndex == index && wayPoints.Length > 1) {
            newIndex = Random.Range(0, wayPoints.Length);
        }

        index = newIndex;
        wait = true;
    }
}

    // Método para mover a plataforma
    void Moving()
    {
        // Move a plataforma em direção ao waypoint atual com a velocidade especificada
        if(wayPoints.Length != 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[index].position, speed * Time.deltaTime);
        }
    }

    public void SetMove(bool state)
    {
        startMove = state;
    }

   
}
