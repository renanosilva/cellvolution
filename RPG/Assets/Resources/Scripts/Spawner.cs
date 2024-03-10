using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Prefab do objeto a ser instanciado
    public GameObject objectPrefab;
    // Referência ao transform do personagem principal (MC)
    private Transform mcTransform;

    // Método chamado quando o script é inicializado
    private void Start()
    {
        // Encontra e armazena a referência ao transform do personagem principal (MC)
        mcTransform = FindObjectOfType<Char>().transform;
    }

    // Método para instanciar o objetoPrefab na posição do personagem principal (MC)
    public void SpawnarObjeto()
    {
        // Instancia o objetoPrefab na posição do personagem principal (MC) com a rotação padrão (identidade)
        Instantiate(objectPrefab, mcTransform.position, Quaternion.identity);
    }
}
