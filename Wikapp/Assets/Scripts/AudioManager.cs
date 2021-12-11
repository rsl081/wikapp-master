using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;
    AudioManager audioManager;
    private void Awake() {

        audioManager = FindObjectOfType<AudioManager>();
        Scene scene = SceneManager.GetActiveScene();


        DontDestroyOnLoad(gameObject);
        if(FindObjectsOfType<AudioManager>().Length > 1){

            Destroy(gameObject);

        }

        EventCenter.GetInstance().AddEventListener("DestroyMyAudioManager", DestroyMyAudioManager);

    }


    private void OnDestroy() {
        
        EventCenter.GetInstance().RemoveEventListener("DestroyMyAudioManager", DestroyMyAudioManager);
    }

    public void  DestroyMyAudioManager()
    {
        Destroy(gameObject);
    }

}
