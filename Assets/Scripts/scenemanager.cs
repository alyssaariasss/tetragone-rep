﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{

    public void LoadScreen(string scenetoload)
    {
        SceneManager.LoadScene(scenetoload);
    }
 
}
