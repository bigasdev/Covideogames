using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public void SetLevel(float sliderValue)
    {
        //como o fader funciona em escala logarítmica, a gente transforma o valor em log
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
