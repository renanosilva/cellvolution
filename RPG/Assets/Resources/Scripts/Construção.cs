using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construção : MonoBehaviour
{
    private Char Char;
    private bool podeAbrir;
    [Header("Menu de Construção")]   
    public GameObject[] telaConstrução;
    [Header("Menu de construção necessario")]
    public Construção[] construcao;
    private int construcaoNecessaria;
    private bool  checkConstrução;

    private int ConstrucaoAtual; // Nível atual da construção

    [SerializeField]
    public float distance;
    public TextoQuest textoMissao;

    [Header("Texto da missão")]
    public string txt;

    private bool missaoConcluida = false; // Variável para indicar se a missão foi concluída

    [Header("Animação da construção ao conclui-la")]
    public Animator anim;
    public Animator anim2;

    private void Start()
    {
        Char = FindObjectOfType<Char>();
    }

    void Update()
    {

        if (construcaoNecessaria >= 0 && construcaoNecessaria < construcao.Length && construcao[construcaoNecessaria] != null)
        {
             checkConstrução = construcao[construcaoNecessaria].GetMissao();
        }else{
            Debug.Log("Nao foi possivel construir");
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
                && telaConstrução[ConstrucaoAtual].activeSelf == false && podeAbrir && !GetMissao()) // Verifica se a missão não está concluída
            {
                telaConstrução[ConstrucaoAtual].SetActive(true);
                Char.DisableControls();
            }else if (collider.gameObject == Char.gameObject && Input.GetKeyDown(KeyCode.C)
                && telaConstrução[ConstrucaoAtual].activeSelf && podeAbrir && !GetMissao()) // Verifica se a missão não está concluída
            {
                telaConstrução[ConstrucaoAtual].SetActive(false);
                Char.DisableControls();
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
        if(anim != null){
            anim.Play("NPAentry");

        }       
    }
    public void SetProximoNivelConstrucao(){
        distance = 1.5f;
        missaoConcluida = false; 
        podeAbrir = false; 
        ConstrucaoAtual++;
    }

    public void ConstruçãoConcluída(){
        Debug.Log("ENTROU NO METODO");
        if(construcao[construcaoNecessaria] != null ){
                if(checkConstrução == true){
                    SetMissaoConcluida(true);
                }else  if(anim2 != null){
                        
                        anim2.Play("NPAentry");

                    }
            
            if(construcaoNecessaria >= 1){
                if(checkConstrução == true){
                    construcaoNecessaria++;

                    if(checkConstrução == true){
                        SetMissaoConcluida(true);
                        if(anim != null){
                            anim.Play("NPAentry");
                        }
                    }
                }else{
                    if(anim2 != null){
                        
                        anim2.Play("NPAentry");

                    }
                }
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
    
}
