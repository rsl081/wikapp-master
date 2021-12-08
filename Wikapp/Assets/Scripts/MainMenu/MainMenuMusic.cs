using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuMusic : MonoBehaviour
{
    private void Awake() {

        if(FindObjectsOfType<AudioManager>().Length > 1){

            Destroy(gameObject);

        }

        EventCenter.GetInstance().AddEventListener("DestroyMyMainMenuMusic", DestroyMyMainMenuMusic);

    }

    private void OnDestroy() {
        
        EventCenter.GetInstance().RemoveEventListener("DestroyMyMainMenuMusic", DestroyMyMainMenuMusic);
    }

    public void DestroyMyMainMenuMusic()
    {
        Destroy(this.gameObject);
    }



}
