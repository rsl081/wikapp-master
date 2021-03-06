using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    Toggle m_Toggle;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject progressPanel;
    private void Awake() {

        m_Toggle = GetComponentInChildren<Toggle>();
        audioSource = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
       
       
        if(PlayerPrefs.GetInt("muted") == 1)
        {
       
                audioSource.Stop();
        
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
        //progressPanel.SetActive(false);
    }

    //Output the new state of the Toggle into Text
    void ToggleValueChanged(Toggle change)
    {
        if(m_Toggle.isOn)
        {
        
            audioSource.Play();
            PlayerPrefs.SetInt("muted", 0);

        }else{
            audioSource.Stop();
            PlayerPrefs.SetInt("muted", 1);

        }
    }

    public void OkayBtn()
    {
        progressPanel.SetActive(true);
    }

    public void CancelBtn()
    {
        progressPanel.SetActive(false);
    }

    public void DeleteProgress()
    {
        Info.Instance.deletePlayerAndProgress();
        progressPanel.SetActive(false);
        Destroy(mainCanvas);
        SceneManager.LoadScene("InformationScene");

    }

    public void TutorialBtn()
    {
        Destroy(mainCanvas);
        SceneManager.LoadScene("TutorialScene");
    }
    public void Credits()
    {
        Destroy(mainCanvas);
        SceneManager.LoadScene("Credits");
    }

}
