using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public AudioMixer mixer;

    float GetVol(float vol)
    {
        float newVol = 0;
        newVol = 20 * Mathf.Log10(vol);
        if (vol <= 0)
        {
            newVol = -80;
        }
        return newVol;
    }

    public void SetMasterVol(float vol)
    {
        mixer.SetFloat("MasterVol", GetVol(vol));
    }

    public void SetMusicVol(float vol)
    {
        mixer.SetFloat("MusicVol", GetVol(vol));
    }

    public void SetSFXVol(float vol)
    {
        mixer.SetFloat("SFXVol", GetVol(vol));
    }
}
