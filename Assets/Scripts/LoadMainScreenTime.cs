using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainScreenTime : MonoBehaviour
{
    [SerializeField]
    private float delayBeforeLoading = 20f;
    [SerializeField]
    private string scenetoload;

    private float timeElapsed;

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed > delayBeforeLoading)
        {
            SceneManager.LoadScene(scenetoload);
        }
    }
        
    
}
