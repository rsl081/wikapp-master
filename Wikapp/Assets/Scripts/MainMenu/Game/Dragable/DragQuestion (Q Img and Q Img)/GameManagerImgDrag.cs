using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerImgDrag : MonoBehaviour
{
    QuizQuestionDrag quiz;
    EndScreen endScreen;
    ScoreKeeper scoreKeeper;
    SingleLevel singleLevel;
    Transition transition;
    private void Awake() {
        
        singleLevel = FindObjectOfType<SingleLevel>();
        quiz = FindObjectOfType<QuizQuestionDrag>();
        endScreen = FindObjectOfType<EndScreen>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        transition = FindObjectOfType<Transition>();

        EventCenter.GetInstance().AddEventListener("UpdateScorePercent", UpdateScore);

        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);

    }
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("UpdateScorePercent", UpdateScore);
        
    }
    void UpdateScore()
    {
        if(quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
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
