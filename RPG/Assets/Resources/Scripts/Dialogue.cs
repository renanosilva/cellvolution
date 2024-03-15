using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    // Referências para os NPCs
    public NPC npc;
    public NPC1 nPC1;
    // Texto UI para exibir o diálogo
    public Text txt;
    // Tempo que leva para cada letra aparecer
    public float cooldown;

    // Referência para o Animator
    private Animator anim;
    // Linha de diálogo selecionada
    private int selected;
    // Diálogo a ser escrito
    private string str;

    // Referência para o script do personagem
    private Char perso;

    private void Start()
    {
        // Obtém a referência para o Animator
        anim = GetComponent<Animator>();
        // Obtém a referência para o script do personagem
        perso = GameObject.Find("MC").GetComponent<Char>();
    }

    // Método para mostrar o diálogo
    public void showDiolog()
    {
        // Ativa a animação de abrir o diálogo
        anim.SetTrigger("open");
        // Reinicia a seleção da linha de diálogo
        selected = 0;
        // Seleciona o diálogo apropriado com base na condição do NPC
        if (npc.condição)
        { 
            str = npc.falas[selected];
        }
        else if (nPC1.condição)
        { 
            str = nPC1.falas[selected];
        }
        // Carrega as letras do diálogo
        loadLetters();
        // Desabilita os controles do personagem durante o diálogo
        perso.DisableControls();
    }

    // Método para carregar as letras do diálogo
    public void loadLetters()
    {
        // Limpa o texto UI
        txt.text = "";
        // Converte a string do diálogo em um array de caracteres
        char[] chars = str.ToCharArray();
        // Itera sbore cada caractere do diálogo
        for (int i = 0; i < chars.Length; i++)
        {
            // Inicia uma coroutine para adicionar cada letra com um atraso
            StartCoroutine(getLetter(chars[i], i));
        }
    }

    // Método para avançar para o próximo diálogo
    public void nextDialog()
    {
        // Verifica a condição do NPC e avança para o próximo diálogo, se disponível
        if (npc.condição)
        {
            if (npc.falas.Count == selected + 1)
            {
                endDialog();
            }
            else
            {
                selected++;
                str = npc.falas[selected];
                loadLetters();
            }
        }
        if (nPC1.condição)
        {
            if (nPC1.falas.Count == selected + 1)
            {
                endDialog();
            }
            else
            {
                selected++;
                str = nPC1.falas[selected];
                loadLetters();
            }
        }
    }

    // Método para encerrar o diálogo
    public void endDialog()
    {
        // Ativa a animação de fechar o diálogo
        anim.SetTrigger("close");
        // Limpa o texto UI
        str = "";
        txt.text = "";
        // Habilita os controles do personagem após o diálogo
        perso.EnableControls();
    }

    // Coroutine para adicionar cada letra com um atraso
    IEnumerator getLetter(char c, int i)
    {
        yield return new WaitForSeconds(cooldown * i);
        txt.text += c.ToString();
    }
}
