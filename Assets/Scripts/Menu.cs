using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource;
    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
