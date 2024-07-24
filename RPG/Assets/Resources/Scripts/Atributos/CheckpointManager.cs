using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class CheckDados
{
    public int indiceCelulaAtiva;
    public int indiceBarreiraAtiva;
    public string textoMissaoAtual;
    public float nivelBarraProgresso;
    public List<string> itensColetados;

    public float x;
    public float y;
    public float z;

    public int[] construcoesAtuais;
    public List<itemInInv> itensInventario;
}

public class CheckpointManager : MonoBehaviour
{

    public static CheckpointManager instance;

    public int indiceCelulaAtiva;
    public int indiceBarreiraAtiva;
    public string textoMissaoAtual;
    public float nivelBarraProgresso;
    public List<string> itensColetados;

    public float x;
    public float y;
    public float z;

    public int[] construcoesAtuais;
    public List<itemInInv> itensInventario;

    private string path;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/salvamento.txt";

        Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        CheckDados data = new CheckDados();

        data.indiceCelulaAtiva = indiceCelulaAtiva;
        data.indiceBarreiraAtiva = indiceBarreiraAtiva;
        data.textoMissaoAtual = textoMissaoAtual;
        data.nivelBarraProgresso = nivelBarraProgresso;
        data.itensColetados = itensColetados;
        data.itensInventario = itensInventario;

        data.x = x;
        data.y = y;
        data.z = z;

        data.construcoesAtuais = construcoesAtuais;

        bf.Serialize(file, data);

        file.Close();
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            CheckDados data = (CheckDados)bf.Deserialize(file);
            file.Close();

            indiceCelulaAtiva = data.indiceCelulaAtiva;
            indiceBarreiraAtiva = data.indiceBarreiraAtiva;
            textoMissaoAtual = data.textoMissaoAtual;
            nivelBarraProgresso = data.nivelBarraProgresso;
            itensColetados = data.itensColetados;
            itensInventario = data.itensInventario;

            x = data.x;
            y = data.y;
            z = data.z;

            construcoesAtuais = data.construcoesAtuais;
        }
    }

    public void Reset()
    {
        indiceCelulaAtiva = 0;
        indiceBarreiraAtiva = 0;
        textoMissaoAtual = "";
        nivelBarraProgresso = 0;
        itensColetados = new List<string>();
        itensInventario = new List<itemInInv>();

        x = 0;
        y = 0;
        z = 0;

        if(construcoesAtuais != null)
        {
            construcoesAtuais = null;
        }

        Save();
    }

}
