using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable] // Permite que a classe PlayerData seja serializável
public class PlayerData
{ 
    // Listas para armazenar dados do jogador
    public List<item> playerItemsDB;
    public List<ItemImage> playerItemImages;
    public List<itemInInv> playerItemInInv;
}

public class GameManager : MonoBehaviour
{
    // Instância estática do GameManager para acesso global
    public static GameManager instance;

    // Listas para armazenar dados do jogador
    public List<item> playerItemsDB;
    public List<ItemImage> playerItemImages;
    public List<itemInInv> playerItemInInv;

    private string path; // Caminho para o arquivo de salvamento

    private void Awake()
    {
        // Gerencia a instância única do GameManager
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        // Define o caminho para o arquivo de salvamento na pasta persistente de dados do aplicativo
        path = Application.persistentDataPath + "/playerSave.sav";
    }

    // Método para salvar os dados do jogador
    public void Save()
    {
        // Inicializa o BinaryFormatter para serializar os dados
        BinaryFormatter bf = new BinaryFormatter();
        // Abre um arquivo para escrita
        FileStream file = File.Create(path);
        // Cria uma instância de PlayerData para armazenar os dados do jogador
        PlayerData data = new PlayerData();

        // Copia os dados das listas do GameManager para a instância de PlayerData
        data.playerItemsDB = playerItemsDB;
        data.playerItemInInv = playerItemInInv;
        data.playerItemImages = playerItemImages;

        // Serializa os dados e escreve no arquivo
        bf.Serialize(file, data);

        // Fecha o arquivo
        file.Close();
    }

    // Método para carregar os dados do jogador
    void Load()
    {
        // Verifica se o arquivo de salvamento existe
        if(File.Exists(path))
        {
            // Inicializa o BinaryFormatter para desserializar os dados
            BinaryFormatter bf = new BinaryFormatter();
            // Abre o arquivo para leitura
            FileStream file = File.Open(path, FileMode.Open);

            // Desserializa os dados do arquivo e os armazena em uma instância de PlayerData
            PlayerData data = (PlayerData)bf.Deserialize(file);
            // Fecha o arquivo
            file.Close();

            // Copia os dados da instância de PlayerData de volta para as listas do GameManager
            playerItemsDB = data.playerItemsDB;
            playerItemInInv = data.playerItemInInv;
            playerItemImages = data.playerItemImages;
        }
    }
}
