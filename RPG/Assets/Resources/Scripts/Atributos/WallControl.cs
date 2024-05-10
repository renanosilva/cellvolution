using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallControl : MonoBehaviour {

    public GameObject barreira;
    public int nivelFortalecimentoRequerido;
    public string textoMensagem;

    public Text texto;
    public Animator caixaMensagem;
    public string nomeAnimacao;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(AtributoManager.instance.nivelFortalecimento >= nivelFortalecimentoRequerido)
        {
            barreira.SetActive(false);
        }
        else
        {
            texto.text = textoMensagem;
            caixaMensagem.Play(nomeAnimacao);
        }
    }
}
