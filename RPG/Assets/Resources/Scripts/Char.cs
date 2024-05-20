using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Char : MonoBehaviour
{
    // Variáveis públicas para configurar o personagem
    [Header("CharConfig")]
    public float Speed; // Velocidade do personagem
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
    public string scene; // Nome da cena

    private void Awake()       
    {
        audioManager = GetComponent<AudioManager>(); // Obtém o componente AudioManager
        anim = GetComponent<Animator>(); // Obtém o componente Animator
        transformSave = GameObject.Find("TransformTpSave").GetComponent<Transform>(); // Encontra e obtém o transform para salvar
    }

    void Start()
    {
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
            backgroundAudio.PlayAudio(orgBG);
        }
        else if (scene == "Organismo")
        {
            // Define a animação, salva posição, troca de animação, habilita controles
            anim.runtimeAnimatorController = Resources.Load("Animations/McAnim/MC") as RuntimeAnimatorController;
            transformSave.position = transform.position;
            transform.position = ms.transform.position;
            anim.Play("TPinvertido");
            backgroundAudio.PlayAudio(cellBG);
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

    // Método chamado a cada frame
    private void Update()
    {
        // Verifica se o personagem pode ser controlado
        if (canControl == true)
        {
            // Verifica as teclas pressionadas para movimento
            if (Input.GetKey(KeyCode.W)) //w
            {
                SetSpeedF(0);
                transform.position += new Vector3(0, Speed, 0);
                SetSpeedB(1);
            }
            else if (Input.GetKey(KeyCode.D)) //D
            {
                SetSpeedL(0);
                transform.position += new Vector3(Speed, 0, 0);
                SetSpeedR(1);
            }
            else if (Input.GetKey(KeyCode.S)) //S
            {
                transform.position += new Vector3(0, -Speed, 0);
                SetSpeedF(1);
            }
            else if (Input.GetKey(KeyCode.A)) //A
            {
                transform.position += new Vector3(-Speed, 0, 0);
                SetSpeedL(1);
            }

            // Verifica se as teclas foram soltas para parar o movimento
            if (Input.GetKeyUp(KeyCode.W))//w
            {
                transform.position += new Vector3(0,0,0);
                SetSpeedB(0);
            }
            else if (Input.GetKeyUp(KeyCode.D)) //D
            {
                transform.position += new Vector3(0,0,0);
                SetSpeedR(0);    
            }
            else if (Input.GetKeyUp(KeyCode.S)) //S
            {
                transform.position += new Vector3(0,0,0);                   
                SetSpeedF(0);
            }
            else if (Input.GetKeyUp(KeyCode.A)) //A
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
}
