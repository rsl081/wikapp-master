using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

//! Responsible for UI such as dragImg, text, and quesHolder.
public class QuizQuestionDrag : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] public Image questionImage;
    [SerializeField] List<QuizImgDragSO> questions = new List<QuizImgDragSO>();    
    QuizImgDragSO currentQuestion;
    [SerializeField] public Transform[] backToSamePosition;

    [Header("Answers")]
    [SerializeField] public GameObject[] answerImg;
    int correctAnswerIndex;

    [Header("Buttons Color")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Scoring")]
    ScoreKeeper scoreKeeper;

    [Header ("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;
    int index = -1;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 1;
        DisplayQuestion();
    }

    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        //SetButtonState(false);
        GetNextQuestion();

    }

    void DisplayAnswer(int index)
    {
        //Image buttonImage;
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            //questionImage.sprite = questionSO.GetQuestion();
            //Coorect Message 
            // buttonImage = answerButton[index].GetComponent<Image>();
            // buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswer();
        }else{

            // buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            // buttonImage.sprite = correctAnswerSprite;
        }
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            ShowCompletion();
        }

    }

    void ShowCompletion()
    {
        EventCenter.GetInstance().EventTrigger("UpdateScorePercent");
    }

    void GetNextQuestion()
    {
        if(questions.Count > 0){
            //SetButtonState(true);
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

            questionImage.sprite = currentQuestion.GetQuestion();

            for(int i = 0; i < answerImg.Length; i++){
                Image btnText = answerImg[i].GetComponentInChildren<Image>();
                if(Vector2.Distance(btnText.gameObject.transform.position, backToSamePosition[i].position) < 3){

                }
                
                btnText.gameObject.transform.position = backToSamePosition[i].position;
                btnText.sprite = currentQuestion.GetAnswer(i);
            }
        }
        
    }


    // void SetButtonState(bool state)
    // {
    //     for(int i = 0; i < answerImg.Length; i++)
    //     {
    //         Image button = answerImg[i].GetComponent<Image>();
    //         button.interactable = state;
    //     }
    // }
}
