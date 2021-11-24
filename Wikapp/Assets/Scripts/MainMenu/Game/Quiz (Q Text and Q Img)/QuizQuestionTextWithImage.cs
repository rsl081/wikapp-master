using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizQuestionTextWithImage : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] Image questionImageHolder;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QDragWithTextSO> questions = new List<QDragWithTextSO>();    
    QDragWithTextSO currentQuestion;
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
    [SerializeField] bool isHoldingSomething;
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

            questionText.text = currentQuestion.GetQuestionStr();
            if(isHoldingSomething)
            {
                questionImageHolder.sprite = currentQuestion.GetQuestionHolder();
            }

            for(int i = 0; i < answerImg.Length; i++){
                TextMeshProUGUI btnText = answerImg[i].GetComponentInChildren<TextMeshProUGUI>();
                btnText.text = currentQuestion.GetAnswer(i);
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
