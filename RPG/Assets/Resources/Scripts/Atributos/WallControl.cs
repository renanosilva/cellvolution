using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class WallControl : MonoBehaviour {

    public GameObject barreira;
    public GameObject proximaBarreira;
    public int nivelForcaMembranaRequerida;
    public string textoMensagem;
    public GameObject objMissoes;
    private Collider2D colisor;

    [Header("Referência à caixa de mensagem")]
    public Text texto;
    public Animator caixaMensagem;
    public string nomeAnimacao;

    [Header("Verifica se a celula foi completada")]
    public bool CheckMissaoCelula;

    [Header("Defina como uma barreira comum")]
    public bool barreiraNormal;

    [Header("Referência ao indice de salvamento do jogo")]
    public int indiceCheckpoint;

    private void Start()
    {
        colisor = GameObject.Find("MC").GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(colisor == collision)
        {
            if (barreiraNormal == true)
            {
                texto.text = "Você ainda não é capaz de chegar a esse lugar!";
                caixaMensagem.Play(nomeAnimacao);
            }else if (barreiraNormal == true && barreira.activeSelf == false)
            {
                proximaBarreira.SetActive(true);
            }
            else if (CheckMissaoCelula == true && !barreiraNormal)
            {
                if (AtributoManager.instance.forcaMembrana >= nivelForcaMembranaRequerida)
                {
                    barreira.SetActive(false);
                    if(proximaBarreira != null){

                        proximaBarreira.SetActive(true);
                    }
                    objMissoes.SetActive(false);
                    
                }
                else
                {
                    texto.text = textoMensagem;
                    caixaMensagem.Play(nomeAnimacao);
                    objMissoes.SetActive(true);
                   
          
                    
                }

            }
        }
        
    }

    public void SetCheckMissao(bool check){
        CheckMissaoCelula = check;
    }
}
