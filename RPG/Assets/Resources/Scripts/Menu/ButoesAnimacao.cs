using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButoesAnimacao : MonoBehaviour
{
    // Configuração da movimentação
    public float amplitude = 0.1f; // Amplitude do balanço
    public float speed = 1.0f; // Velocidade do balanço

    // Posição original da imagem
    private Vector3 originalPosition;

    void Start()
    {
        // Salva a posição original da imagem
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calcula a posição vertical baseada em uma função senoidal
        float yOffset = Mathf.Sin(Time.time * speed) * amplitude;
        // Aplica a posição vertical à imagem
        transform.position = originalPosition + new Vector3(0, yOffset, 0);
    }
}
