using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixer mixer;

    float GetVol(float vol)
    {
        float newVol = 20 * Mathf.Log10(vol);
        if (vol <= 0)
        {
            newVol = -80;
        }
        return newVol;
    }

    // Funções para ajustar os volumes e salvar as configurações
    public void SetMasterVol(float vol)
    {
        mixer.SetFloat("MasterVol", GetVol(vol));
        PlayerPrefs.SetFloat("MasterVol", vol);
        PlayerPrefs.Save();
    }

    public void SetMusicVol(float vol)
    {
        mixer.SetFloat("MusicVol", GetVol(vol));
        PlayerPrefs.SetFloat("MusicVol", vol);
        PlayerPrefs.Save();
    }

    public void SetSFXVol(float vol)
    {
        mixer.SetFloat("SFXVol", GetVol(vol));
        PlayerPrefs.SetFloat("SFXVol", vol);
        PlayerPrefs.Save();
    }

    // Função para carregar as configurações de volume ao iniciar o jogo
    void LoadVolumeSettings()
    {
        float masterVol = PlayerPrefs.GetFloat("MasterVol", 1.0f);
        float musicVol = PlayerPrefs.GetFloat("MusicVol", 1.0f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVol", 1.0f);

        SetMasterVol(masterVol);
        SetMusicVol(musicVol);
        SetSFXVol(sfxVol);
    }

    void Start()
    {
        LoadVolumeSettings();
    }
} 