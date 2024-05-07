using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atributos : MonoBehaviour {

    private int velocidade;
    private int forcaMembrana;
    private int nivelComunicacao;

    public Text textoVelocidade;
    public Text textoForcaMembrana;
    public Text textoNivelComunicacao;

    public GameObject painelAtributos;
    

    // Start is called before the first frame update
    void Start()
    {
        velocidade = AtributoManager.instance.velocidade;
        forcaMembrana = AtributoManager.instance.forcaMembrana;
        nivelComunicacao = AtributoManager.instance.nivelComunicacao;


        textoVelocidade.text = "Velocidade: " + AtributoManager.instance.velocidade;
        textoForcaMembrana.text = "Força da Membrana: " + AtributoManager.instance.forcaMembrana;
        textoNivelComunicacao.text = "Nivel de comunicação: " + AtributoManager.instance.nivelComunicacao;
    }

    private void FixedUpdate()
    {
        mostrarAtributos();
    }

    public void mostrarAtributos()
    {
        if(!painelAtributos.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                painelAtributos.SetActive(true);
            }
        }
        else if(painelAtributos.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                painelAtributos.SetActive(false);
            }
        }
    }


    public void SetVelocidade(int velocidade)
    {
        this.velocidade = velocidade;
        AtributoManager.instance.velocidade = velocidade;
        textoVelocidade.text = "Velocidade: " + velocidade;
    }

    public void SetForcaMembrana(int forcaMembrana)
    {
        this.forcaMembrana = forcaMembrana;
        AtributoManager.instance.forcaMembrana = forcaMembrana;
        textoForcaMembrana.text = "Força da Membrana: " + forcaMembrana;
    }

    public void SetNivelComunicacao(int nivelComunicacao)
    {
        this.nivelComunicacao = nivelComunicacao;
        AtributoManager.instance.nivelComunicacao = nivelComunicacao;
        textoNivelComunicacao.text = "Nivel de comunicação: " + nivelComunicacao;
    }

    public int GetVelocidade()
    {
        return velocidade;
    }

    public int GetForcaMembrana(){
        return forcaMembrana;
    }

    public int GetNivelComunicacao(){
        return nivelComunicacao;
    }

}
