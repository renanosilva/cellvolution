using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Declaração de uma variável pública que armazenará a referência a um componente AudioSource.
    public AudioSource audioSource;

    public static AudioManager Instance { get; private set; }  // MODIFICADO


    // Awake() é chamado quando o script é inicializado.
    private void Awake()
    {
        if (Instance == null) // MODIFICADO
        {
            // Se não estiver, define esta instância como a instância do Singleton e a torna persistente entre cenas.
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else // MODIFICADO
        {
            // Se já houver uma instância, destrói esta nova instância para manter apenas uma.
            Destroy(gameObject); // MODIFICADO
        }

    }

    // Método público que reproduz um AudioClip recebido como parâmetro.
    public void PlayAudio(AudioClip clip)
    {
        // Define o AudioClip recebido como o clip a ser reproduzido pelo AudioSource.
        audioSource.clip = clip;
        // Inicia a reprodução do AudioClip através do AudioSource.
        audioSource.Play();
    }

    // Método público para definir o volume do AudioSource. // MODIFICADO
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    // Método público para obter o volume atual do AudioSource. // MODIFICADO
    public float GetVolume()
    {
        return audioSource.volume;
    }
}
