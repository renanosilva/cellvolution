using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysVoadores : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        damageable = GetComponent<damageable>();

        // Posicione o inimigo no ponto inicial
        transform.position = startPoint.position;   
    }

    void FixedUpdate(){
        health = damageable.GetHealth(); // Obtém a saúde
        Move();
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
}
