using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMusic : MonoBehaviour
{
    AudioSource audioSource;
    MainMenuMusic mainMenuMusic;
    private void Awake() {
        
        mainMenuMusic = FindObjectOfType<MainMenuMusic>();
        audioSource = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
        if(FindObjectsOfType<AudioManager>().Length > 1){

            Destroy(gameObject);

        }

        if(mainMenuMusic != null)
        {
            EventCenter.GetInstance().EventTrigger("DestroyMyMainMenuMusic");
        }

        if(PlayerPrefs.GetInt("muted") == 1)
        {
            audioSource.Stop();
        }



        
        EventCenter.GetInstance().AddEventListener("DestroyGameMusic", DestroyGameMusic);

    }


    private void OnDestroy() {
        
        EventCenter.GetInstance().RemoveEventListener("DestroyGameMusic", DestroyGameMusic);
    }

    public void DestroyGameMusic()
    {
        Destroy(gameObject);
    }

}
