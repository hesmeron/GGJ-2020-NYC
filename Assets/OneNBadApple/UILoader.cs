﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
       SceneManager.LoadScene("levelOne", LoadSceneMode.Additive);
    }
}
