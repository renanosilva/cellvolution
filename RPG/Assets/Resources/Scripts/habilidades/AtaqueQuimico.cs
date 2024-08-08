using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AtaqueQuimico : MonoBehaviour
{
    // Referência à câmera principal usada para converter a posição do mouse em coordenadas de mundo
    public Camera camaraAtaque;

    // Armazena a posição do mouse em coordenadas de mundo
    private Vector3 mousePos;

    // Prefab do bullet que será instanciado e movido
    public GameObject bullet;
    public GameObject bulletShot;

    // Collider circular representando o raio permitido para o movimento do bullet
    public CircleCollider2D raio;

    // Eventos para ativar e desativar o ataque
    public UnityEvent OnAttack;
    public UnityEvent ReleaseAttack;

    // Taxa de ataque em segundos
    public float attackRate = 0.5f;

    // Indica se o ataque está ativo
    public bool atackActive;

    // Taxa de disparo em segundos
    public float fireRate = 0.25f;

    // Impulso aplicado ao bullet ao disparar
    public float shotImpulse = 10;

    public int numberShot = 25;
    // Tempo para o próximo disparo permitido
    private float nextFire;

    // Indica se o ataque pode ser realizado
    private bool canAttack = true;
    private Collider2D colider;

    public GameObject canvaTimer;
    public BarrasController barraTempo;
    public float timerRecarga = 10;
    public bool recarga;
    public GameObject purificacao;
    public transformacao transformacao;
    private Char personagem;

    public Text NUmberText;
    public GameObject canvaText;
    private Animator anim;

    private void Start()
    {
        colider = GetComponent<Collider2D>();

        barraTempo.vidaAtual = timerRecarga;
        barraTempo.vidaMaxima = timerRecarga;

        numberShot = 25;
        personagem = FindObjectOfType<Char>();
        anim = GetComponent<Animator>();
    }

    // Método chamado uma vez por frame
    void Update()
    {
        // Obter a posição do mouse em coordenadas de mundo
        mousePos = camaraAtaque.ScreenToWorldPoint(Input.mousePosition);

        // Garantir que a posição z seja zero para evitar problemas de rotação em 2D
        mousePos.z = 0;

        // Calcular a direção e a distância do centro do círculo ao mouse
        Vector3 direction = mousePos - raio.transform.position;
        float distance = direction.magnitude;

        // Verificar se a distância é maior que o raio do CircleCollider2D
        if (distance > raio.radius)
        {
            // Ajustar a direção para que o bullet fique dentro do raio
            direction = direction.normalized * raio.radius;
        }

        // Definir a posição do bullet para a posição ajustada
        bullet.transform.position = raio.transform.position + direction;

        // Calcular a rotação
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Aplicar a rotação ao transform do objeto que contém o script
        transform.rotation = Quaternion.Euler(0, 0, rotZ);


        if (Input.GetMouseButtonDown(0) && atackActive == true && bullet.activeSelf == true && numberShot > 0 && purificacao.activeSelf == false && transformacao.IsTransformed() == true && personagem.scene == "Organismo"
            )
        {
            Fire();
            string number = numberShot.ToString();
            NUmberText.text = number;
            canvaText.SetActive(true);
        } else if (bullet.activeSelf == false)
        {
            canvaText.SetActive(false);
        }

        if (numberShot == 0)
        {
            recarga = true;
            canvaTimer.SetActive(true);
            timerRecarga -= Time.deltaTime;
            canvaText.SetActive(false);

            barraTempo.vidaAtual = timerRecarga;

            if (timerRecarga <= 0)
            {
                numberShot = 25;
                timerRecarga = 10;
                canvaTimer.SetActive(false);
                recarga = false;
            }
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
         damageable dama = collision.GetComponent<damageable>();
        if (collision.gameObject.CompareTag("Enime"))
        {
            dama.TakeDamage(10, 0);
            Destroy(bulletShot, 10f);
        }
    }
    // Método para disparar o bullet
    public void Fire()
    {
        // Verificar se o ataque está ativo
        if (atackActive == false)
            return;

        // Verificar se o tempo atual é maior que o tempo permitido para o próximo disparo
        if (Time.time > nextFire)
        {
            // Atualizar o tempo para o próximo disparo permitido
            nextFire = Time.time + fireRate;

            // Instanciar um novo bullet na posição e rotação atuais
            GameObject newBullet = Instantiate(bulletShot, bullet.transform.position, bullet.transform.rotation);

            // Garantir que o newBullet tem um Rigidbody2D para aplicar o impulso
            Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Adicionar um impulso ao novo bullet na direção atual
                rb.AddForce(transform.right * shotImpulse, ForceMode2D.Impulse);
                numberShot--;
            }

            // Ignorar colisões entre o newBullet e o CircleCollider2D
            Physics2D.IgnoreCollision(newBullet.GetComponent<Collider2D>(), raio);
        }
    }

    // Método chamado ao finalizar o ataque
    void FinishAttack()
    {
        // Permitir novos ataques
        canAttack = true;

        // Invocar o evento de liberar o ataque
        ReleaseAttack.Invoke();
    }

    public bool GetRecarga()
    {
        return recarga;
    }

    public void SetAttackActive()
    {
        atackActive = true; 
        anim.Play("NPAentry");
    }
}