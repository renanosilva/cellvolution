using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public Text textMOstrado;
    private float timeValue = 0;
    public bool timerOver;

    public PurificacaoCelular purificacaoCelular;
    public AtaqueQuimico ataqueQuimico;


    [SerializeField]
    private BarrasController barraDeVida;


    void Start()
    {
        timerOver = false;

        barraDeVida.vidaAtual = timeValue;
        barraDeVida.vidaMaxima = purificacaoCelular.timerAttack;
    }
    void Update()
    {
        TimeCount();

        if (purificacaoCelular.GetIsAttackActive() == true && ataqueQuimico.GetRecarga() == false)
        {
            textMOstrado.text = "Duração do ataque: ";
            timeValue = purificacaoCelular.GetTimer();
            barraDeVida.vidaAtual = timeValue;
        }

        if (purificacaoCelular.GetIsAttackActive() == false && purificacaoCelular.GetReactivationTimer() > 0f && ataqueQuimico.GetRecarga() == false)
        {
            textMOstrado.text = "Reativação do ataque: ";
            timeValue = purificacaoCelular.GetReactivationTimer();
            barraDeVida.vidaAtual = timeValue;
            barraDeVida.vidaMaxima = purificacaoCelular.reactivationDelay;

        }

        if (purificacaoCelular.GetIsAttackActive() == false && ataqueQuimico.GetRecarga() == true)
        {
            Debug.Log("Entrou na condicao");
            textMOstrado.text = "Recarregamendo: ";
            timeValue = ataqueQuimico.timerRecarga;
            barraDeVida.vidaAtual = timeValue;
            barraDeVida.vidaMaxima = 10;
        }


    }


    void TimeCount()
    {
        timerOver = false;

        if (!timerOver && timeValue > 0)
        {
            timeValue -= Time.deltaTime;
            barraDeVida.vidaAtual = timeValue;

            if (timeValue <= 0)
            {
                timeValue = 0;
                timerOver = true;
            }
        }
    }


}
