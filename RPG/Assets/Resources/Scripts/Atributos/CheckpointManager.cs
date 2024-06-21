﻿using System.Collections;
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

    public float x;
    public float y;
    public float z;
}

public class CheckpointManager : MonoBehaviour
{

    public static CheckpointManager instance;

    public int indiceCelulaAtiva;
    public int indiceBarreiraAtiva;
    public string textoMissaoAtual;
    public float nivelBarraProgresso;

    public float x;
    public float y;
    public float z;

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

        path = Application.persistentDataPath + "/CheckSave.txt";

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

        data.x = x;
        data.y = y;
        data.z = z;

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

            x = data.x;
            y = data.y;
            z = data.z;
        }
    }

    public void Reset()
    {
        indiceCelulaAtiva = 0;
        indiceBarreiraAtiva = 0;
        textoMissaoAtual = "";
        nivelBarraProgresso = 0;

        x = 0;
        y = 0;
        z = 0;

        Save();
    }

}