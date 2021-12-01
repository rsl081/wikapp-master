using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMusic : MonoBehaviour
{
     AudioSource audioSource;
     MainMenuMusic mainMenuMusic;
    private void Awake() {
        
        mainMenuMusic = FindObjectOfType<MainMenuMusic>();

        DontDestroyOnLoad(gameObject);
        if(FindObjectsOfType<AudioManager>().Length > 1){

            Destroy(gameObject);

        }

        if(mainMenuMusic != null)
        {
            EventCenter.GetInstance().EventTrigger("DestroyMyMainMenuMusic");
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
