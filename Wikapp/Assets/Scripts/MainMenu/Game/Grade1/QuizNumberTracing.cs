using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizNumberTracing : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] GameObject questionImage;
    [SerializeField] List<TracingNumberSO> questions = new List<TracingNumberSO>();    
    TracingNumberSO currentQuestion;

    [Header("Answers")]
   // [SerializeField] GameObject[] answerButton;
    int correctAnswerIndex;

    [Header("Scoring")]
    ScoreKeeper scoreKeeper;

    [Header ("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;
    int index = -1;
    public int lenOfTracing;
    public int numberOfCorrectTrace;
    GameObject letter;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 1;
        DisplayQuestion();
        lenOfTracing = currentQuestion.GetQuestion().GetComponentsInChildren<Image>().Length;

    }


    public void OnAnswerSelected()
    {
        DisplayAnswer();
        GetNextQuestion();

    }

    public void DisplayAnswer()
    {
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            ShowCompletion();
        }
        else if(numberOfCorrectTrace == lenOfTracing-1){
            scoreKeeper.IncrementCorrectAnswer();
        }
        
        Destroy(letter.gameObject);
    }

    void ShowCompletion()
    {
        EventCenter.GetInstance().EventTrigger("UpdateScorePercent");
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0){
            //SetDefaultButtonSprite();
            //GetRandomQuestion();
            DisplayQuestion();
            
            progressBar.value++;
            scoreKeeper.IncrementQuesitonsSeen();
        }
    }
    
    void DisplayQuestion()
    {
        if(index < questions.Count-1){
            index++;
        }else{
            return;
        }
        if(questions.Count > 0){
            //int index = Random.Range(0, questions.Count);
            currentQuestion = questions[index];
            letter = Instantiate(currentQuestion.GetQuestion(), questionImage.transform.position,
                    Quaternion.identity) as GameObject;
            letter.transform.SetParent(GameObject.FindGameObjectWithTag("QuizCanvas").transform, false);
            
            
            
            //questionImage = currentQuestion.GetQuestion();


        }
        
    }

}
