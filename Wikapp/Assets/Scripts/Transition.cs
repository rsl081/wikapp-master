using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public float timeToWait = 3f;
    public string sceneName;
    [SerializeField] bool isLoadAutoStart = true;
    int currentSceneIndex;
    void Start()
    {
        if(EventCenter.GetInstance() == null){

            EventCenter.GetInstance().Clear();
            EventCenter.GetInstance().EventTrigger("PressStarButton");
            EventCenter.GetInstance().EventTrigger("UpdateMap");
        }

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
       
        if(currentSceneIndex == 1 && isLoadAutoStart)
        {
            StartCoroutine(WaitForTime());

        }else if(Info.Instance.isGameSaved() && currentSceneIndex == 0){

            LoadNextScene();
        
        }
    }

    IEnumerator WaitForTime()
    {
        
        yield return new WaitForSeconds(timeToWait);

        if(Info.Instance.isGameSaved())
        {
            StringSceneToLoad();
        }else{
            LoadNextScene();
            
        }
      
    }

    public void TutorialBtn()
    {
        if(Info.Instance.isGameSaved())
        {
            FindObjectOfType<VoiceManager>().StopVoice();
            StringSceneToLoad();
        }else{
            FindObjectOfType<VoiceManager>().StopVoice();
            LoadNextScene();
        }
        EventCenter.GetInstance().EventTrigger("DestroyMyAudioManager");
    }
    public void CreditsBtn()
    {
        if(Info.Instance.isGameSaved())
        {
            StringSceneToLoad();
        }else{
            LoadNextScene();
        }
    }

    public void SubmitInfo()
    {
        if(Info.Instance.isGameSaved())
        {
            StringSceneToLoad();
        }
        else{
            LoadNextScene();
        }
        EventCenter.GetInstance().EventTrigger("DestroyMyAudioManager");
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

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://kaizen081.itch.io/learning-is-fun");
    }

}
