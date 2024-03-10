using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Declaração de uma variável pública que armazenará a referência a um componente AudioSource.
    public AudioSource audioSource;

    // Awake() é chamado quando o script é inicializado.
    private void Awake()
    {
        // Obtém a referência ao componente AudioSource anexado a este GameObject.
        audioSource = GetComponent<AudioSource>();
    }

    // Método público que reproduz um AudioClip recebido como parâmetro.
    public void PlayAudio(AudioClip clip)
    {
        // Define o AudioClip recebido como o clip a ser reproduzido pelo AudioSource.
        audioSource.clip = clip;
        // Inicia a reprodução do AudioClip através do AudioSource.
        audioSource.Play();
    }
}
