using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSync : MonoBehaviour
{
    private AudioSource localAudioSource;
    private float initialVolume;

    private void Start()
    {
        localAudioSource = GetComponent<AudioSource>();

        // Verifica se o AudioSource foi encontrado
        if (localAudioSource == null)
        {
            Debug.LogError("AudioSource não encontrado no GameObject " + gameObject.name);
            return;
        }
        initialVolume = localAudioSource.volume; // Salva o volume inicial da cena Organismo
        SetVolume(AudioManager.Instance.GetVolume()); // Sincroniza o volume inicial

        // Desativa todos os outros AudioSources nas outras cenas
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.gameObject.scene != gameObject.scene && audioSource != localAudioSource)
            {
                audioSource.enabled = false;
            }
        }
    }

    private void SetVolume(float volume)
    {
        if (localAudioSource != null)
        {
            localAudioSource.volume = volume * initialVolume; // Ajusta o volume local considerando o volume global
        }
    }
}