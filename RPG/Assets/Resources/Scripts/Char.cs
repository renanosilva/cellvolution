using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Char : MonoBehaviour
{
    // Variáveis públicas para configurar o personagem
    [Header("CharConfig")]
    public float Speed; // Velocidade do personagem

    private float SpeedInicial;
    public float health; 
    public float onTime;
    public bool OnTochEnime;

    [Header("Imports")]
    public Camera cam; // Referência para a câmera
    public TextoQuest texto; // Referência para o texto da quest
    private bool canControl = true; // Flag para controlar se o personagem pode se mover

    // Variáveis para gerenciar áudios
    [Header("Audios")]
    public AudioClip orgBG; // Áudio para o fundo do organismo
    public AudioClip cellBG; // Áudio para o fundo da célula
    public AudioClip itemColetado; // Áudio para quando um item é coletado
    public AudioClip ISF; // Áudio para a habilidade de ISF
    public AudioClip Construído; // Áudio para quando algo é construído
    public AudioManager backgroundAudio; // Gerenciador de áudio para o fundo
    public AudioManager audioManager; // Gerenciador de áudio

    // Variáveis para controlar a movimentação
    private Vector3 goTo = new Vector3(0, 0, -10); // Destino de movimento
    private Animator anim; // Referência para o componente Animator
    private Transform transformSave; // Posição para salvar transformação
    public Quest quest; // Referência para a quest
    public ManagerScenes ms; // Gerenciador de cenas

    [Header("Purificacao Celular - Attack do player")]
    public PurificacaoCelular purificacaoCelular;

    public GameObject canvaTimer;
    public GameObject canvaEnergia;

    public BarrasController barraDeTempo;

    public BarrasController barraDeEnergia;
    private float energia;

    public string scene; // Nome da cena
    
    public transformacao transformacao;

    private bool transformed = false;
    private damageable damageable;
    private void Awake()       
    {
        audioManager = GetComponent<AudioManager>(); // Obtém o componente AudioManager
        anim = GetComponent<Animator>(); // Obtém o componente Animator
        transformSave = GameObject.Find("TransformTpSave").GetComponent<Transform>(); // Encontra e obtém o transform para salvar
        damageable = GetComponent<damageable>();
        backgroundAudio = GetComponent<AudioManager>();
    }

    void Start()
    {
        SpeedInicial = Speed;
        if(purificacaoCelular != null){

            energia = purificacaoCelular.energiaUsada;
        }
        // Não há funcionalidades específicas no Start neste código
    }

    // Corotina para trocar animações
    public void trocarAnimadores()
    {
        if (scene == "Dentro")
        {
            // Define a animação, desabilita controles, aguarda e troca de animação
            EnableControls();
            anim.Play("TP");
            anim.runtimeAnimatorController = Resources.Load("Animations/McAnim/MC Celula") as RuntimeAnimatorController;
            transform.position = transformSave.position;
           
        }
        else if (scene == "Organismo")
        {
            // Define a animação, salva posição, troca de animação, habilita controles
            anim.runtimeAnimatorController = Resources.Load("Animations/McAnim/MC") as RuntimeAnimatorController;
            transformSave.position = transform.position;
            transform.position = ms.transform.position;
            anim.Play("TPinvertido");
            if(backgroundAudio != null){

                backgroundAudio.PlayAudio(cellBG);
            }
            DisableControls();
            Invoke("EnableControls", 2.5f);
        }
    }

    // Método para definir velocidade de frente
    private void SetSpeedF(int speedF)
    {
        anim.SetInteger("SpeedF", speedF);
    }

    // Método para definir velocidade de trás
    private void SetSpeedB(int speedB)
    {
        anim.SetInteger("SpeedB", speedB);
    }

    // Método para definir velocidade lateral esquerda
    private void SetSpeedL(int speedL)
    {
        anim.SetInteger("SpeedL", speedL);
    }

    // Método para definir velocidade lateral direita
    private void SetSpeedR(int speedR)
    {
        anim.SetInteger("SpeedR", speedR);
    }

    // Método para desabilitar controles
    public void DisableControls()
    {
        canControl = false;
        SetSpeedF(0);
        SetSpeedB(0);
        SetSpeedL(0);
        SetSpeedR(0);
    }

    // Método para habilitar controles
    public void EnableControls()
    {
        canControl = true;
    }

    public void OnExitTochEnime(){
        Speed = SpeedInicial;
        OnTochEnime = false;
    }
    

    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag == "Enime" && OnTochEnime == false ){
            Speed -= 1f/100f;
            OnTochEnime = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision){


        if(collision.gameObject.tag == "Enime"){
            Invoke("OnExitTochEnime", onTime);
        }
    }

    void ativarTransformacao(){
        transformed = !transformed;
    }
    // Método chamado a cada frame
    private void Update()
    {
        barraDeEnergia.vidaAtual = transformacao.GetCurrentEnergy();

        transformacao.SetIsTransformed(transformed);
        if(Input.GetKeyDown(KeyCode.K)){
            if(transformacao.IsInCooldown() == false && transformacao.transformBloque && purificacaoCelular.GetIsAttackActive() == false && barraDeEnergia.vidaAtual > 0f){
                ativarTransformacao();
                anim.SetTrigger("OnTransformacao"); 

            }else if(transformed == false){
                transformacao.ActivateTransformation(false);
            }

        }

        if(barraDeEnergia.vidaAtual < 100f){

            canvaEnergia.gameObject.SetActive(true);
        }

        health = damageable.GetHealth();

        if(scene == "Dentro"){

            if(purificacaoCelular != null){
                purificacaoCelular.AtivarReactiveAttack(true);
                barraDeTempo.vidaAtual = purificacaoCelular.timerAttack;
                purificacaoCelular.SetIsAttackActive(false);
                canvaTimer.gameObject.SetActive(false);
            }
             
        }

        // Verifica se o personagem pode ser controlado
        if (canControl == true)
        {
            // Verifica as teclas pressionadas para movimento
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) //w
            {
                SetSpeedF(0);
                transform.position += new Vector3(0, Speed, 0);
                SetSpeedB(1);
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) //D
            {
                SetSpeedL(0);
                transform.position += new Vector3(Speed, 0, 0);
                SetSpeedR(1);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) //S
            {
                transform.position += new Vector3(0, -Speed, 0);
                SetSpeedF(1);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) //A
            {
                transform.position += new Vector3(-Speed, 0, 0);
                SetSpeedL(1);
            }

            // Verifica se as teclas foram soltas para parar o movimento
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))//w
            {
                transform.position += new Vector3(0,0,0);
                SetSpeedB(0);
            }
            else if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) //D
            {
                transform.position += new Vector3(0,0,0);
                SetSpeedR(0);    
            }
            else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow)) //S
            {
                transform.position += new Vector3(0,0,0);                   
                SetSpeedF(0);
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow)) //A
            {
                transform.position += new Vector3(0,0,0);                    
                SetSpeedL(0);
            }
        }

        // Verifica se há uma quest em andamento
        if (quest != null)
        {
            if (quest.isActive)
            {
                // Verifica o tipo de objetivo da quest
                if (quest.objetivo.tipoObjetivo == TipoObjetivo.Colete)
                {
                    quest.objetivo.ProgressoColeta(quest.objetivo.QuantRequerida, quest.objetivo.idItem);
                }
                if (quest.objetivo.tipoObjetivo == TipoObjetivo.pressioneBotão && Input.GetKeyDown(quest.objetivo.keyCode))
                {
                    for (int i = 0; i < quest.objetivo.QuantAtual.Count; i++)
                    {
                        quest.objetivo.QuantAtual[i]++;
                    }
                }
                // Verifica se a quest foi completada
                if (quest.objetivo.completou() == true)
                {
                    quest.Completo();
                }
            }
        }
    }

    public bool GetCanControl()
    {
        return canControl;
    }

    public bool GetTransformed()
    {
        return transformed;
    }
}
