using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColoringGameManager : MonoBehaviour
{
    PaintBucket paintBucket;
    EndScreen endScreen;
    ScoreKeeper scoreKeeper;
    SingleLevel singleLevel;
    Transition transition;
    private void Awake() {
        
        singleLevel = FindObjectOfType<SingleLevel>();
        paintBucket = FindObjectOfType<PaintBucket>();
        endScreen = FindObjectOfType<EndScreen>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        transition = FindObjectOfType<Transition>();

        EventCenter.GetInstance().AddEventListener("UpdateScorePercent", UpdateScore);

        paintBucket.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);

    }
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("UpdateScorePercent", UpdateScore);
        
    }
    void UpdateScore()
    {
        Invoke(nameof(DelayUpdatingScore), 1f);
    }

    void DelayUpdatingScore()
    {

        if(paintBucket.isComplete)
        {
            paintBucket.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
            endScreen.ShowStarGUI();
        }
    }
  

    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToHome()
    {
        endScreen.PressCandiesBackHome();
    }
    public void NextLevel()
    {
        transition.LoadNextScene();
    }
}
