﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public float timeToWait = 3f;
    [SerializeField] string sceneName;
    [SerializeField] bool isLoadAutoStart = true;
    int currentSceneIndex;
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex == 0 && isLoadAutoStart)
        {
            StartCoroutine(WaitForTime());
        }
    }

    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void StringSceneToLoad()
    {
        SceneManager.LoadScene(sceneName);
    }

    public float getTimeToWait()
    {
        return timeToWait;
    }

}
