using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


[Serializable]
public class PlayerDados
{
    public int velocidade;
    public int forcaMembrana;
    public int nivelComunicacao;
}


public class AtributoManager : MonoBehaviour {

    public static AtributoManager instance;

    public int velocidade;
    public int forcaMembrana;
    public int nivelComunicacao;

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
        }
    }

}
