using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameMusic : MonoBehaviour
{
     AudioSource audioSource;
    private void Awake() {


        DontDestroyOnLoad(gameObject);
        if(FindObjectsOfType<AudioManager>().Length > 1){

            Destroy(gameObject);

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
