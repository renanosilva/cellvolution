using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class WallControl : MonoBehaviour {

    public GameObject barreira;
    public int nivelForcaMembranaRequerida;
    public string textoMensagem;

    public GameObject objMissoes;
    public Text texto;
    public Animator caixaMensagem;
    public string nomeAnimacao;
    private bool checkatributos;
    [Header("Verifica se a celula foi completada")]
    public bool CheckMissaoCelula;

    private void Update(){
        if(checkatributos == true){
            if(objMissoes != null){
                objMissoes.SetActive(true);
            }

        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        checkatributos = true;
        if(CheckMissaoCelula == true){
            if(AtributoManager.instance.forcaMembrana >= nivelForcaMembranaRequerida)
            {
                barreira.SetActive(false);
                objMissoes.SetActive(false);
            }
            else
            {
                texto.text = textoMensagem;
                caixaMensagem.Play(nomeAnimacao);
            }

        }
    }

    public void SetCheckMissao(bool check){
        CheckMissaoCelula = check;
    }
}
