using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionMenuUI;
    public AudioMixer bgMixer;
    public AudioMixer soundMixer;
    public void Option()
    {
        optionMenuUI.SetActive(true);
    }

    public void SetSFX(float volume)
    {
        soundMixer.SetFloat("FXVolume", volume);
    }

    public void SetVolume(float volume)
    {
        bgMixer.SetFloat("BGVolume", volume);
    }

    public void Cancel()
    {
        optionMenuUI.SetActive(false);
    }

}
