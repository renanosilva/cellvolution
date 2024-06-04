using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Construir : MonoBehaviour
{
    // Referência para o inventário
    public Inventory inventory;
    // Array de itens necessários para a construção
    public Item[] items;
    // Referência para o animador
    public Animator anim;
    // Referência para o botão falso
    public GameObject fakeButton;
    // Referência para o script do personagem
    private Char pessoa;
    // Evento Unity chamado quando a construção é concluída
    public UnityEvent onConstruction;

    [Header("Requisitos")]
    // Listas para guardar os requisitos dos itens
    public List<int> QuantAtual;
    public List<int> QuantRequerida;
    public List<int> idItem;

    [Header("item a ser melhorado")]
    // Parâmetros para melhorar um item
    public int aumento;
    public int idAumento;

    // Awake é chamado quando o script é inicializado
    public void Awake()
    {
        // Encontra e atribui referências para itens e para o script do personagem
        items = GameObject.FindObjectsOfType<Item>();
        pessoa = GameObject.FindObjectOfType<Char>();
    }

    // Verifica se há itens suficientes para a construção
    public bool itensSuficientes()
    {
        bool r = false;
        for (int i = 0; i < QuantRequerida.Count; i++)
        {
            if (QuantAtual[i] >= QuantRequerida[i])
            {
                r = true;
            }
            else
            {
                // Se não houver itens suficientes, reproduz a animação e o áudio
                Debug.LogWarning("Itens insuficientes!");
                if(anim != null){
                     anim.Play("ISFentry");

                }
                pessoa.audioManager.PlayAudio(pessoa.ISF);
                r = false;
                break;
            }
        }
        return r;
    }

    // Consome os itens necessários para a construção
    public void consumirItens(List<int> id)
    {
        bool t = false;
        for (int i = 0; i < inventory.itemInInv.Count; i++)
        {
            int idA = inventory.itemInInv[i].id;
            for(int l = 0; l < QuantAtual.Count; l++)
            {
                if (idA == id[l])
                {
                    inventory.itemInInv[i].count -= QuantRequerida[l];
                    t = true;
                    break;
                }
            }
        }
        if (t == false)
        {
            print("Item não encontrado!");
        }
    }

    // Atualiza o progresso dos itens
    public void ProgressoItens(List<int> itensNecessarios, List<int> idN)
    {
        for (int i = 0; i < inventory.itemInInv.Count; i++)
        {
            int id = inventory.itemInInv[i].id;
            for (int l = 0; l < QuantAtual.Count; l++)
            {
                if (idN[l] == id)
                {
                    QuantAtual[l] = inventory.itemInInv[i].count;
                }
            }
        }
        QuantRequerida = itensNecessarios;
    }

    // Verifica se é possível construir
    public bool podeConstruir()
    {
        bool v = false;
        if (itensSuficientes())
        {
            consumirItens(idItem);
            v = true;
        }
        else
        {
            Debug.LogWarning("Itens insuficientes!");
            v = false;
        }
        return v;
    }

    // Aumenta o multiplicador de coleta de um item
    public void aumentarMutiplicadorDeColeta()
    {
        for(int i = 0; i< items.Length; i++)
        {
            if(items[i].id == idAumento)
            {
                items[i].mutiplicador += aumento;
                Debug.Log("certo");
                this.gameObject.SetActive(false);
                fakeButton.SetActive(true);
            }
        }   
    }

    // Atualiza o progresso dos itens
    public void Update()
    {
        ProgressoItens(QuantRequerida,idItem);
    }

    // Gasta os itens para construir
    public void GastarConstruir()
    {
        if (podeConstruir())
        {
            // Se é possível construir, reproduz o áudio e chama o evento de construção
            Debug.Log("proximo");
            pessoa.audioManager.PlayAudio(pessoa.Construído);
            onConstruction.Invoke();
        }
        else
        {
            Debug.LogWarning("Itens insuficientes! Não é possível consumir os itens!");
            Debug.Log("ISF");
        }
    }
}
