using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// Classe que define um item
[System.Serializable]
public class item
{
    public string name; // Nome do item
    public string description; // Descrição do item
    public enum Type // Enumeração para o tipo de item
    {
        Arma,
        Armadura,
        Consumivel,
        Material,
        Chave
    }
    public Type tipo; // Tipo do item
    public int value; // Valor do item
}

// Classe que define um item no inventário
[System.Serializable]
public class itemInInv
{
    public int id; // ID do item
    public int count; // Quantidade do item
    public int mutiplicador; // Multiplicador para quantidade
    public Sprite imagemDoItem; // Imagem do item
}

// Classe principal do inventário
public class Inventory : MonoBehaviour
{
    public List<item> ItemsDB = new List<item>(); // Lista de itens disponíveis no jogo
    public List<ItemImage> itemImages = new List<ItemImage>(); // Lista de imagens de itens

    [Header("Player")]
    public List<itemInInv> itemInInv = new List<itemInInv>(); // Lista de itens no inventário do jogador

    [Header("Canvas")]
    public GameObject invScene; // Referência ao objeto do inventário no cenário
    public Text itens; // Texto para mostrar os itens
    public Text description; // Texto para mostrar a descrição do item
    public ItemBG itemBG; // Background do item selecionado

    // Variáveis auxiliares
    private List<string> itensToShow = new List<string>(); // Lista de itens a serem mostrados
    private int selected; // Índice do item selecionado
    bool opened; // Indica se o inventário está aberto
    private Char persoa; // Referência ao personagem
    private AudioManager audioManager; // Gerenciador de áudio
    public AudioClip abriu; // Som de abertura do inventário
    public AudioClip seleção; // Som de seleção de item

    public Image imagemItemSelecionado;


    // Inicialização
    
    
    private void Start()
    {
        // Inicialização de variáveis e componentes
        audioManager = GetComponent<AudioManager>();
        persoa = GetComponent<Char>();
        invScene = GameObject.Find("Inventario");
        itens = GameObject.Find("Itens").GetComponent<Text>();
        description = GameObject.Find("Descricao").GetComponent<Text>();
        itemBG = GameObject.Find("itemBG").GetComponent<ItemBG>();
        invScene.SetActive(false);

    }

    // Método para adicionar um item ao inventário
    public void addItem(int id, int count, int mutiplicador, Sprite imagem)
    {
        // Verifica se o item já está no inventário
        bool t = false;
        for (int i = 0; i < itemInInv.Count; i++)
        {
            if (itemInInv[i].id == id)
            {
                // Atualiza a quantidade do item
                itemInInv[i].count += count * mutiplicador;
                t = true;
                break;
            }
        }
        // Se o item não estiver no inventário, adiciona-o
        if (t == false)
        {
            itemInInv iii = new itemInInv();
            iii.id = id;
            iii.mutiplicador = mutiplicador;
            iii.count = count * mutiplicador;
            iii.imagemDoItem = imagem;

            itemInInv.Add(iii);
        }
    }

    // Método para remover um item do inventário
    public void remItem(int id, int count)
    {
        // Verifica se o item está no inventário
        bool t = false;
        for (int i = 0; i < itemInInv.Count; i++)
        {
            if (itemInInv[i].id == id)
            {
                // Reduz a quantidade do item
                itemInInv[i].count -= count;
                // Se a quantidade for menor ou igual a zero, remove o item
                if (itemInInv[i].count <= 0)
                {
                    itemInInv.Remove(itemInInv[i]);
                }
                t = true;
                break;
            }
        }
        // Se o item não for encontrado, exibe uma mensagem
        if (t == false)
        {
            print("Item não encontrado!");
        }
    }

    // Método para verificar se um item está no inventário e se há quantidade suficiente
    public int verifyItem(int id, int count)
    {
        bool t = false;
        bool c = false;
        for (int i = 0; i < itemInInv.Count; i++)
        {
            if (itemInInv[i].id == id)
            {
                // Se a quantidade do item for suficiente, define 'c' como verdadeiro
                if (itemInInv[i].count >= count)
                {
                    c = true;
                }
                t = true;
                break;
            }
        }
        // Retorna um código baseado na verificação do item
        int v;
        if (t == false)
        {
            v = 0; // Item não encontrado
        }
        else if (c == false)
        {
            v = 1; // Item encontrado, mas sem quantidade suficiente
        }
        else
        {
            v = 2; // Item encontrado com quantidade suficiente
        }
        return v;
    }

    // Método para mostrar/ocultar o inventário
    public void inventoryShow(bool option)
    {
        if (option == true)
        {
            // Prepara o inventário para ser mostrado
            if (itensToShow != null)
            {
                itensToShow.Clear();
            }
            else 
            {
                itensToShow = new List<string>();
            }

            selected = 0;
            invScene.SetActive(true);
            audioManager.PlayAudio(abriu);
            persoa.DisableControls();
            for (int i = 0; i < itemInInv.Count; i++)
            {
                int id = itemInInv[i].id;
                itemImages[i].ItemInvsprite = itemInInv[i].imagemDoItem;
                if (i == 0)
                    itensToShow.Add(ItemsDB[id].name + " / " + itemInInv[i].count);
                else
                    itensToShow.Add(ItemsDB[id].name + " / " + itemInInv[i].count);
            }

        }
        else
        {
            // Oculta o inventário
            invScene.SetActive(false);
            persoa.EnableControls();
        }
    }

    // Atualização do inventário
    private void Update()
    {
        if (opened)
        {
            // Verificações de entrada de teclado para navegar nos itens
            int itembgpointer = itemBG.animator.GetInteger("TargetItem");

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (selected > 0)
                {
                    selectedItem(-1);
                    itemBG.animator.SetInteger("TargetItem", itembgpointer - 1);
                    itemBG.animator.SetFloat("Direção", -1f);
                    audioManager.PlayAudio(seleção);
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (selected < itensToShow.Count - 1)
                {
                    selectedItem(+1);
                    itemBG.animator.SetInteger("TargetItem", itembgpointer + 1);
                    itemBG.animator.SetFloat("Direção", 1f);
                    audioManager.PlayAudio(seleção);
                }
            }

        }
        // Verificação da tecla Tab para mostrar/ocultar o inventário
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryShow(!opened);
            reloadInv();
            opened = !opened;
        }

    }

    // Método para selecionar um item
    public void selectedItem(int item)
    {
        selected += item;
        reloadInv();
    }

    // Método para recarregar o inventário
    public void reloadInv()
    {
        itens.text = "";
        if (selected >= 0 && selected < itensToShow.Count)
        {
            for (int i = 0; i < itensToShow.Count; i++)
            {
                itens.text = itensToShow[selected];
                imagemItemSelecionado.sprite = itemInInv[selected].imagemDoItem;
                imagemItemSelecionado.color = new Color32(255, 255, 255, 255);
            }
            if (itemInInv[selected].id >= 0 && itemInInv[selected].id < ItemsDB.Count)
            {
                description.text = ItemsDB[itemInInv[selected].id].description;
            }
            else
            {
                description.text = "Descrição não disponível";
            }
        }
    }

}
