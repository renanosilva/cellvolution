using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // Permite que a classe seja serializável e exibida no Unity Inspector
public class ItemImage : MonoBehaviour
{
    // Sprite que representa o item no inventário
    public Sprite ItemInvsprite;
    // Referência para o componente Image do GameObject
    private Image image;

    void Start()
    {
        // Obtém a referência para o componente Image do GameObject
        image = gameObject.GetComponent<Image>();
    }

    // Método chamado a cada frame
    private void Update()
    {
        // Define a sprite do componente Image como a sprite do item
        image.sprite = ItemInvsprite;
        
        // Verifica se a sprite não é nula para garantir que a imagem seja exibida corretamente
        if (!image.sprite.Equals(null))
        {
            // Define a cor do componente Image como totalmente visível (opacidade 255)
            image.color = new Color32(255, 255, 255, 255);
        }
    } 
}
