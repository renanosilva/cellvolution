using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class PlayerDados
{
    //Atributos referentes às habilidades
    public int velocidade;
    public int forcaMembrana;
    public int nivelComunicacao;

    //Atributos referentes às barreiras
    public int nivelFortalecimento;
}


public class AtributoManager : MonoBehaviour {

    public static AtributoManager instance;

    public Boolean bloquearTela;

    [Header("Atributos referentes às habilidades")]
    public int velocidade;
    public int forcaMembrana;
    public int nivelComunicacao;

    [Header("Atributos referentes às barreiras")]
    public int nivelFortalecimento;

    private string path;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        path = Application.persistentDataPath + "/playerSave.txt";

        Load();
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(path);
        PlayerDados data = new PlayerDados();

        data.velocidade = velocidade;
        data.forcaMembrana = forcaMembrana;
        data.nivelComunicacao = nivelComunicacao;

        data.nivelFortalecimento = nivelFortalecimento;

        bf.Serialize(file, data);

        file.Close();
    }

    public void Load()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);

            PlayerDados data = (PlayerDados)bf.Deserialize(file);
            file.Close();

            velocidade = data.velocidade;
            forcaMembrana = data.forcaMembrana;
            nivelComunicacao = data.nivelComunicacao;

            nivelFortalecimento = data.nivelFortalecimento;
        }
    }

    public void Reset()
    {
        velocidade = 0;
        forcaMembrana = 0;
        nivelComunicacao = 0;
        nivelFortalecimento = 0;

        Save();
    }

    

}
