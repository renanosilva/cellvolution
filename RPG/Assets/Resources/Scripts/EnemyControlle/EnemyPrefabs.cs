using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefabs : MonoBehaviour
{

    // Saúde do inimigo
    public float health;
    // Referência ao objeto do jogador
    public GameObject Player;
    // Velocidade de movimento do inimigo
    public float speed;
    // Distância mínima para o jogador que ativa certos comportamentos
    public float distancePlayer;

    // Referência ao objeto do inimigo pai
    public Transform enemyFather;

    // Referência ao componente Rigidbody2D do inimigo
    private Rigidbody2D rb;

    // Referência ao componente SpriteRenderer do inimigo
    private SpriteRenderer sprite;

    // Referência ao componente damageable do inimigo
    private damageable damageable;

    // Referência ao script PurificacaoCelular no jogador
    public PurificacaoCelular purificacao;
    public GameObject dialogue;

    private bool startMove = true;

    // Use this for initialization
    void Start()
    {
        // Obtém o componente Rigidbody2D anexado ao GameObject
        rb = GetComponent<Rigidbody2D>();
        // Obtém o componente SpriteRenderer anexado ao GameObject
        sprite = GetComponent<SpriteRenderer>();
        // Obtém o componente damageable anexado ao GameObject
        damageable = GetComponent<damageable>();
    }

    // Chamado em intervalos fixos para atualizações de física
    void FixedUpdate()
    {

        if (dialogue.gameObject.activeSelf == true || AtributoManager.instance.bloquearTela == true)
        {
            startMove = false;
        }
        else if (dialogue.gameObject.activeSelf == false && AtributoManager.instance.bloquearTela == false)
        {
            startMove = true;
        }
        // Obtém a saúde atual do inimigo
        health = damageable.GetHealth();
        float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

        // Chama o método Move para atualizar o movimento do inimigo
        if (startMove && distanceToPlayer < distancePlayer)
        {
            Move();
        }

        if (distanceToPlayer > distancePlayer)
        {

            Debug.Log("Entrou no metodo de seguir o obj pai");
            FollowEnemyFather(); // Chama o método FollowEnemyFather para seguir o inimigo pai
        }

    }

    // Método para mover o inimigo
    void Move()
    {
        if (health > 0)
        {
            // Calcula a distância entre o jogador e o inimigo
            float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

            // Verifica se a distância ao jogador é menor que a distância definida e se o ataque do jogador está ativo
            if (distanceToPlayer < distancePlayer && purificacao.GetIsAttackActive())
            {
                Flee(); // Chama o método Flee para fugir
            }
            else if (distanceToPlayer < distancePlayer)
            {
                FollowPlayer(); // Chama o método FollowPlayer para seguir o jogador
            }
            
        }
        else if (health <= 0)
        {
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

    // Método para seguir o inimigo pai
    void FollowEnemyFather()
    {
        // Move o inimigo em direção ao inimigo pai
        if (enemyFather != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemyFather.position, speed * Time.deltaTime);

            // Inverte o sprite dependendo da posição do inimigo pai
            sprite.flipX = transform.position.x > enemyFather.position.x;
        }
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

    // Método para definir o pai do minienemy
    public void SetEnemyFather(Transform father)
    {
        enemyFather = father;
    }
    // Método para tratar a morte do inimigo
    void EnemyDeath()
    {
        health = 0;
        speed = 0;

        // Destruir os componentes do inimigo e o próprio GameObject
        Destroy(transform.gameObject.GetComponent<CircleCollider2D>());
        Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1f);
    }

    public void SetMove(bool state)
    {
        startMove = state;
    }
}
