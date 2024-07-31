using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construção : MonoBehaviour
{

    public int id;
    private Char Char;
    private bool podeAbrir;
    [Header("Menu de Construção")]   
    public GameObject[] telaConstrução;
    
    public int ConstrucaoAtual; // Nível atual da construção
    [Header("Menu de construção necessario")]
    public Construção construcao;
    public Construção construção2; 
    public bool  checkConstrução;
    public bool  checkConstrução2;
    private Inventory inv;
    public GameObject dialogueObj;
    private bool verificadorInv;
    private bool verificadorDialogue;

    [SerializeField]
    public float distance;
    public TextoQuest textoMissao;

    [Header("Texto da missão")]
    public string txt;

    public bool missaoConcluida = false; // Variável para indicar se a missão foi concluída

    [Header("Animação da construção ao conclui-la")]
    public Animator anim;
 

    private void Start()
    {
        Char = FindObjectOfType<Char>();
        inv = FindObjectOfType<Inventory>();
    }

    void Update()
    {
        if ( construcao != null)
        {
             checkConstrução = construcao.GetMissao();
        }
        if(construção2 != null){
            checkConstrução2 = construção2.GetMissao();
        }

        if(dialogueObj.activeSelf == true && inv.gameObject.activeSelf == true){
            verificadorDialogue = true;
            verificadorInv = true;
        }else{
            verificadorDialogue = false;
            verificadorInv = false;
        }
        
       

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance);

        // Verifica se o personagem está dentro do círculo e atende às condições
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == Char.gameObject && podeAbrir && !txt.Equals(""))
            {
                textoMissao
                .desc.text = txt;
            }

            if (collider.gameObject == Char.gameObject && Input.GetKeyDown(KeyCode.C)
                && telaConstrução[ConstrucaoAtual].activeSelf == false && podeAbrir && !GetMissao() && verificadorDialogue == false && verificadorInv == false) // Verifica se a missão não está concluída
            {
                AtributoManager.instance.bloquearTela = true;
                telaConstrução[ConstrucaoAtual].SetActive(true);
                Char.DisableControls();
            } else if (collider.gameObject == Char.gameObject && Input.GetKeyDown(KeyCode.C)
                && telaConstrução[ConstrucaoAtual].activeSelf && podeAbrir && !GetMissao()) // Verifica se a missão não está concluída
            {
                telaConstrução[ConstrucaoAtual].SetActive(false);
                AtributoManager.instance.bloquearTela = false;

                Char.EnableControls();
            }
        }
    }

    public void SetPodeAbrirTrue()
    {
        podeAbrir = true;
    }

    // Método para definir o estado da missão
    public void SetMissaoConcluida(bool concluida)
    {
        distance = 0;
        missaoConcluida = concluida;
        Char.EnableControls();
        AtributoManager.instance.bloquearTela = false;
        inv.SetVerificadorConstrucoes(false);
        if(anim != null){
            anim.Play("NPAentry");

        }     
    }
    public void SetProximoNivelConstrucao(){
        distance = 1.5f;
        missaoConcluida = false; 
        podeAbrir = false; 
        ConstrucaoAtual++;
        inv.SetVerificadorConstrucoes(false);
        AtributoManager.instance.bloquearTela = false;
    }

    public void ConstruçãoConcluída(){
        if(construcao != null && construção2 == null){
                if(checkConstrução == true){
                    SetMissaoConcluida(true);
                    
                }
        }    
        if(construcao == null && construção2 != null){
            if(checkConstrução2 == true){
                SetMissaoConcluida(true);
            }
        }        
        if(construcao != null && construção2 != null){
            if(checkConstrução == true && checkConstrução2 == true){
                SetMissaoConcluida(true);

            }
        }   
       
    }

    public bool GetMissao()
    {
        return missaoConcluida;
    }

    public void SetCheckConstrução(bool check)
    {
        checkConstrução = check;
    }
    public bool GetActive(){
        return podeAbrir;
    }

    public void DesbloquearTela()
    {
        AtributoManager.instance.bloquearTela = false;
    }
    
}
