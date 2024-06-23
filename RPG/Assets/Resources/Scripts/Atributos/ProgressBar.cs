using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float valorMinimo;
    public float valorMaximo;
    public float valorAtual;

    public Text texto;
    public Image fillImage;

    public Animator animator;

    private bool salvarProgresso;

    private void Start()
    {
        valorAtual = CheckpointManager.instance.nivelBarraProgresso;
        AtualizarValores();
        SetAumentoValorAtual(0);
    }

    public void AtualizarValores()
    {
        float valor = valorAtual - valorMinimo;
        float total = valorMaximo - valorMinimo;
        float fill = valor / total;

        fillImage.fillAmount = fill;
        texto.text = valorAtual + "%";
    }

    public void SetAumentoValorAtual(float state)
    {
        StartCoroutine(AtualizarProgressoConstantemente(state));
    }

    IEnumerator AtualizarProgressoConstantemente(float state)
    {
        animator.Play("Aparecer");

        for (int i = 0; i < state; i++)
        {
            yield return new WaitForSeconds(0.15f);
            valorAtual++;
            AtualizarValores();
        }

        if(state != 0)
        {
            AudioManager som = GameObject.Find("MC").GetComponent<AudioManager>();
            som.PlayAudio(Resources.Load<AudioClip>("audios/barraProgresso"));
        }

        if (salvarProgresso == true)
        {
            SetNivelProgresso();
            CheckpointManager.instance.Save();
        }

        yield return new WaitForSeconds(3f);
        animator.Play("Desaparecer");
    }

    private void SetNivelProgresso()
    {
        CheckpointManager.instance.nivelBarraProgresso = valorAtual;
    }

    public void SalvarBarraProgresso(bool state)
    {
        salvarProgresso = state;
    }
}
