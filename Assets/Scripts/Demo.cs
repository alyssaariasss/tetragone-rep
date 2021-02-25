using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Demo : MonoBehaviour
{
    public void Xbutton()
    {
        SceneManager.LoadScene("MainScreen");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("TutorialVideo");
    }
}
