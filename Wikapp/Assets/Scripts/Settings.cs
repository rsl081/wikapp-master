using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Toggle m_Toggle;
    [SerializeField] AudioSource[] audioSource;
    private void Awake() {
        //PlayerPrefs.DeleteAll();
        m_Toggle = GetComponentInChildren<Toggle>();
       // GameObject music = GameObject.FindGameObjectWithTag("Music");
        

        audioSource = FindObjectsOfType<AudioSource>();
       
        if(PlayerPrefs.GetInt("muted") == 1)
        {
            foreach(AudioSource audio in audioSource)
            {
                audio.Stop();
            }
        
            if(m_Toggle != null)
            {

                m_Toggle.isOn = false;
            }
   
        }
    }


    void Start()
    {
        if(m_Toggle != null)
        {

            m_Toggle.onValueChanged.AddListener(delegate {
                ToggleValueChanged(m_Toggle);
            });
        
        }
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        if(m_Toggle.isOn)
        {
        
            foreach(AudioSource audio in audioSource)
            {
                audio.Play();
            }
            PlayerPrefs.SetInt("muted", 0);

        }else{
            foreach(AudioSource audio in audioSource)
            {
                audio.Stop();
            }
            PlayerPrefs.SetInt("muted", 1);

        }
    }
}
