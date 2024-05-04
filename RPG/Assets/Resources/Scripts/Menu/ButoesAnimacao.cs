using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButoesAnimacao : MonoBehaviour
{

    public float speed = 1f; // Velocidade de movimento da imagem de fundo
    public float amplitude = 1f; // Amplitude do movimento vertical
    private Vector3 posicaoIncial;

    void Start()
    {
        posicaoIncial = transform.position;
    }

    void Update()
    {
        // Calcula a posição vertical baseada no tempo, na velocidade e na amplitude do movimento
        float posicaoV = Mathf.Sin(Time.time * speed) * amplitude;

        // Aplica o deslocamento na posição do objeto
        transform.position = posicaoIncial + new Vector3(0, posicaoV, 0);
    }
}
