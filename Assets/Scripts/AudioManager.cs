using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource myAudio;

    // Start is called before the first frame update
    public void Start()
    {
        myAudio = GetComponent<AudioSource>();
        Invoke("playAudio", 5.0f);
    }

    public void playAudio()
    {
        myAudio.Play();
    }
}
