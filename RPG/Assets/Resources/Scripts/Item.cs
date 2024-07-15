using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private Checkpoint checkpoint;

    // Identificador único do item
    public int id;
    // Quantidade do item
    public int count;
    // Multiplicador associado ao item
    public int mutiplicador;
    // Imagem do item
    public Sprite imagemDoItem;

    // Referência para o script do personagem
    private Char Char;
    // Referência para o inventário
    private Inventory inv;

    // Distância mínima para interagir com o item
    [SerializeField]
    public float distance;

    // Método chamado quando o objeto é inicializado
    void Start()
    {
        // Obtém a referência para o script do personagem
        Char = FindObjectOfType<Char>();
        // Obtém a referência para o inventário
        inv = FindObjectOfType<Inventory>();
        checkpoint = GameObject.Find("Checkpoint").GetComponent<Checkpoint>();
    }

    // Método chamado a cada frame
    void Update()
    {
        // Verifica se o personagem está próximo o suficiente para interagir com o item
        if (Vector2.Distance(Char.transform.position, transform.position) < distance)
        {
            // Adiciona o item ao inventário
            inv.addItem(id, count, mutiplicador, imagemDoItem);
            // Reproduz o áudio de coleta do item
            Char.audioManager.PlayAudio(Char.itemColetado);
            checkpoint.itensColetados.Add(gameObject.name + "");
            // Destroi o objeto do item após ser coletado
            Destroy(gameObject);            
        }
    }
}
