using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health;
    public GameObject Player; 
    public float speed;
    public float distancePlayer;

    public Transform startPoint;

    private Rigidbody2D rb;
    private CircleCollider2D circle;
    
    private SpriteRenderer sprite;

    private damageable damageable;

    public PurificacaoCelular purificacao;

    public float fireRate;
    private float nextFireTime;  // Adicionado para controlar o tempo da próxima instância

    public Rigidbody2D enemyPrefab;
    public Transform shotSpawn;

    private Animator anim;

    // Lista para rastrear clones do enemyPrefab
    private List<Rigidbody2D> enemyClones = new List<Rigidbody2D>();

    public Enemy() {}

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        damageable = GetComponent<damageable>();

        // Posicione o inimigo no ponto inicial
        transform.position = startPoint.position;   
        nextFireTime = Time.time; // Inicialize o próximo tempo de disparo para o tempo atual
    }

    void FixedUpdate(){  
        health = damageable.GetHealth(); // Obtém a saúde
        Move();

        if (enemyPrefab != null) {
            if (health > 0) {
                float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);

                if (distanceToPlayer <= 3f && Time.time >= nextFireTime) {
                    Fire();
                    nextFireTime = Time.time + fireRate; // Atualize o próximo tempo de disparo
                }
            } else {
                EnemyDeath();
                Debug.LogWarning("Vida do inimigo zerada!");
            }
        }
    }
    
    void Move(){
        if (health > 0) {
            float distanceToPlayer = Vector3.Distance(Player.transform.position, transform.position);
            
            if (distanceToPlayer < distancePlayer && purificacao.GetIsAttackActive()) {
                Flee();
            } else if (distanceToPlayer < distancePlayer) {
                FollowPlayer();
            } else {
                ReturnToStartPoint();
            }
        } else {
            EnemyDeath();
            Debug.LogWarning("Vida do inimigo zerada!");
        }
    }

    void FollowPlayer() {
        transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);

        sprite.flipX = transform.position.x > Player.transform.position.x;
    }

    void ReturnToStartPoint() {
        transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);

        sprite.flipX = transform.position.x > startPoint.position.x;
    }

    void Flee() {
        Vector3 directionAwayFromPlayer = transform.position - Player.transform.position;
        Vector3 fleePosition = transform.position + directionAwayFromPlayer.normalized * speed * Time.deltaTime;
        
        transform.position = fleePosition;

        sprite.flipX = transform.position.x > Player.transform.position.x;
    }

     void EnemyDeath() {
        health = 0;
        speed = 0;

        // Destruir todos os clones
        foreach (var clone in enemyClones) {
            if (clone != null) {
                Destroy(clone.gameObject);
            }
        }
        enemyClones.Clear(); // Limpar a lista após destruir os clones

        Destroy(transform.gameObject.GetComponent<CircleCollider2D>());
        Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
        Destroy(gameObject, 1f); 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Attack") {
            health--;

            if (health > 0) {
                damageable.StartDamageSprite();
            } else {
                EnemyDeath();
            }
        }
    }

    void Fire() {
        Rigidbody2D newClone = Instantiate(enemyPrefab, shotSpawn.position, transform.rotation);
        newClone.gameObject.SetActive(true); // Ativa o clone
        enemyClones.Add(newClone); // Adiciona o clone à lista
    }
}
