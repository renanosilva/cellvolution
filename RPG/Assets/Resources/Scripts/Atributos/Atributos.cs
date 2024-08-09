using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atributos : MonoBehaviour {

    //atributos referentes às habilidades
    private int velocidade = 1;
    private int forcaMembrana;
    private int nivelComunicacao;

    //atributos referentes às barreiras
    private int nivelFortalecimento;

    [Header("Objetos do painel de atributos")]
    public Text textoVelocidade;
    public Text textoForcaMembrana;
    public Text textoNivelComunicacao;
    public GameObject painelAtributos;
    public GameObject textoMissoes;
    

    // Start is called before the first frame update
    void Start()
    {

        //Atributos referentes à habilidades
        velocidade = AtributoManager.instance.velocidade;
        forcaMembrana = AtributoManager.instance.forcaMembrana;
        nivelComunicacao = AtributoManager.instance.nivelComunicacao;

        //Atributos referentes às barreiras
        nivelFortalecimento = AtributoManager.instance.nivelFortalecimento;

        //Substituindo os textos dos objetos do painel de atributos
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
                textoMissoes.SetActive(false);
            }
        }
        else if(painelAtributos.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                painelAtributos.SetActive(false);
                textoMissoes.SetActive(true);
            }
        }
    }


    public void SetAumentoVelocidade(int velocidade)
    {
        this.velocidade = this.velocidade + velocidade;
        AtributoManager.instance.velocidade = this.velocidade;
        textoVelocidade.text = "Velocidade: " + this.velocidade;
    }

    public void SetAumentoForcaMembrana(int forcaMembrana)
    {
        this.forcaMembrana = this.forcaMembrana + forcaMembrana;
        AtributoManager.instance.forcaMembrana = this.forcaMembrana;
        textoForcaMembrana.text = "Força da Membrana: " + this.forcaMembrana;
    }

    public void SetAumentoNivelComunicacao(int nivelComunicacao)
    {
        this.nivelComunicacao = this.nivelComunicacao + nivelComunicacao;
        AtributoManager.instance.nivelComunicacao = this.nivelComunicacao;
        textoNivelComunicacao.text = "Nivel de comunicação: " + this.nivelComunicacao;
    }

    public void SetAumentoNivelFortalecimento(int nivelFortalecimento)
    {
        this.nivelFortalecimento = this.nivelFortalecimento + nivelFortalecimento;
        AtributoManager.instance.nivelFortalecimento = this.nivelFortalecimento;
    }

    public void SalvarNoArquivo()
    {
        AtributoManager.instance.Save();
    }

    public void BloquearTela()
    {
        AtributoManager.instance.bloquearTela = true;
    }

    public void DesbloquearTela()
    {
        AtributoManager.instance.bloquearTela = false;
    }


}
