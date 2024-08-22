using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour {

    [Header("Objetos que serão ativados ou desativos no inicio do jogo")]
    public GameObject[] celulaAtiva;
    public GameObject[] barreiraAtiva;

    [Header("Scripts que contém o indice de checkpoint")]
    public WallControl[] barreirasJogo;
    public Dialogue1[] celulasHistoria;

    public int[] construcoesAtuais;
    public Construção[] objetosConstrucoes;
    public bool[] construcoesConcluidas;

    public Text textoMissoes;

    //variáveis que armazenam os valores carregados do arquivo de save
    private string textoMissaoAtual;
    private int indiceCelulaAtiva;
    private int indiceBarreiraAtiva;

    //variáveis que armazenam a posição do transform do jogador
    private float x;
    private float y;
    private float z;
    public Vector3 NovaPosicaoJogador;
    private GameObject posicaoAtualJogador;

    [Header("Referência à seta que guia o jogador")]
    public GameObject setaGuia;
    public TargetIndicator setaGuiaScript;

    public List<string> itensColetados;

    public Inventory inventory;
    public List<itemInInv> itensInvent;


    // Start is called before the first frame update
    void Start()
    {

        inventory = GameObject.Find("MC").GetComponent<Inventory>();

        if (CheckpointManager.instance.itensInventario != null)
        {
            Debug.Log("Carregando inventario");
            CarregarInventario();
        } 

        if (!CheckpointManager.instance.textoMissaoAtual.Equals(""))
        {
            textoMissoes.text = CheckpointManager.instance.textoMissaoAtual;
        } 

        indiceCelulaAtiva = CheckpointManager.instance.indiceCelulaAtiva;
        indiceBarreiraAtiva = CheckpointManager.instance.indiceBarreiraAtiva;
       

        //Carregando as células e as barreiras ativas do último salvamento
        CarregarCelulasAtivas();
        CarregarBarreirasAtivas();
        VerificarMissaoBarreira();

        //Atribuindo ao jogador a posição que ele estava na última vez que salvou o jogo
        NovaPosicaoJogador.x = CheckpointManager.instance.x;
        NovaPosicaoJogador.y = CheckpointManager.instance.y;
        NovaPosicaoJogador.z = CheckpointManager.instance.z;

        //Ativando o objeto da seta guia e mandando ela seguir a célula do capítulo salvo
        setaGuia.gameObject.SetActive(true);
        setaGuiaScript.NextTarget(celulaAtiva[indiceCelulaAtiva].transform);


        if(CheckpointManager.instance.construcoesAtuais != null && CheckpointManager.instance.construcoesConcluidas != null)
        {
            CarregarConstrucoes();
        }

        if (CheckpointManager.instance.itensColetados == null)
        {
            itensColetados = new List<string>();
        }
        else
        {
            itensColetados = CheckpointManager.instance.itensColetados;
            CheckpointManager.instance.itensColetados = null;
        }

        DestruirItensColetados();


    }

    public void CarregarCelulasAtivas()
    {
        foreach (Dialogue1 percorrer in celulasHistoria)
        {
            if(percorrer.indiceCheckpoint == indiceCelulaAtiva)
            {
                celulaAtiva[percorrer.indiceCheckpoint].SetActive(true);
            }
            else
            {
                celulaAtiva[percorrer.indiceCheckpoint].SetActive(false);
            }
        }
    }

    public void CarregarBarreirasAtivas()
    {
        foreach(WallControl percorrer in barreirasJogo)
        {
            if(percorrer.indiceCheckpoint == indiceBarreiraAtiva)
            {
                barreiraAtiva[percorrer.indiceCheckpoint].SetActive(true);
            }
            else
            {
                barreiraAtiva[percorrer.indiceCheckpoint].SetActive(false);
            }
        }
    }

    public void SetIndiceCelulaAtiva(int state)
    {
        indiceCelulaAtiva = state;
        CheckpointManager.instance.indiceCelulaAtiva = state;
    }

    public void SetIndiceBarreiraAtiva(int state)
    {
        indiceBarreiraAtiva = state;
        CheckpointManager.instance.indiceBarreiraAtiva = state;
    }

    public void SalvarTextoMissaoAtual()
    {
        textoMissaoAtual = textoMissoes.text;
        CheckpointManager.instance.textoMissaoAtual = textoMissoes.text;
    }


    public void SalvarNoArquivo()
    {
        CheckpointManager.instance.Save();
    }

    public void GuardarPosicaoJogador()
    {

        posicaoAtualJogador = GameObject.Find("MC");
        NovaPosicaoJogador = posicaoAtualJogador.transform.position;

        x = NovaPosicaoJogador.x;
        y = NovaPosicaoJogador.y;
        z = NovaPosicaoJogador.z;

        CheckpointManager.instance.x = NovaPosicaoJogador.x;
        CheckpointManager.instance.y = NovaPosicaoJogador.y;
        CheckpointManager.instance.z = NovaPosicaoJogador.z;

    }

    public void SalvarConstrucoes()
    {
        foreach(Construção posicao in objetosConstrucoes)
        {
            construcoesAtuais[posicao.id] = posicao.ConstrucaoAtual;
            construcoesConcluidas[posicao.id] = posicao.missaoConcluida;
        }

        CheckpointManager.instance.construcoesAtuais = construcoesAtuais;
        CheckpointManager.instance.construcoesConcluidas = construcoesConcluidas;
    }

    public void CarregarConstrucoes()
    {
        construcoesAtuais = CheckpointManager.instance.construcoesAtuais;
        construcoesConcluidas = CheckpointManager.instance.construcoesConcluidas;

        foreach(Construção construcao in objetosConstrucoes)
        {
            construcao.ConstrucaoAtual = construcoesAtuais[construcao.id];
            construcao.missaoConcluida = construcoesConcluidas[construcao.id];
        }
    }

    public void VerificarMissaoBarreira()
    {
        if(barreirasJogo[indiceBarreiraAtiva].barreiraNormal == false)
        {
            barreirasJogo[indiceBarreiraAtiva].CheckMissaoCelula = true;
        }
    }

    public void DestruirItensColetados()
    {

        if (itensColetados != null)
        {
            foreach (string item in itensColetados)
            {
                GameObject itemColetado = GameObject.Find(item);
                Destroy(itemColetado);
            }

        }

    }

    public void SalvarItensColetados()
    {
        CheckpointManager.instance.itensColetados = itensColetados;
    }

    public void SalvarItensInv()
    {
        inventory = GameObject.Find("MC").GetComponent<Inventory>();
        itensInvent = inventory.itemInInv;
        CheckpointManager.instance.itensInventario = itensInvent;
    }

    public void CarregarInventario()
    {
        StartCoroutine(CarregarInventarioCorrotina());
    }

    IEnumerator CarregarInventarioCorrotina()
    {
        yield return new WaitForSeconds(1.5f);
        itensInvent = CheckpointManager.instance.itensInventario;
        inventory.itemInInv = itensInvent;
        CheckpointManager.instance.itensInventario = null;
    }

    public void CarregarItensAoPausar()
    {
        CheckpointManager.instance.Load();
    }
    

}
