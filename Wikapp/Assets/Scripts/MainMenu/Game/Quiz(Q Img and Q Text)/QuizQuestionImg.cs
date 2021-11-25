using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizQuestionImg : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] Image questionImage;
    [SerializeField] List<QuestionImgSO> questions = new List<QuestionImgSO>();    
    QuestionImgSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
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
        SetButtonState(false);
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
            SetButtonState(true);
            //SetDefaultButtonSprite();
            //GetRandomQuestion();
            DisplayQuestion();
            
            progressBar.value++;
            scoreKeeper.IncrementQuesitonsSeen();
        }
    }

    // void GetRandomQuestion()
    // {
    //     int index = Random.Range(0, questions.Count);
    //     currentQuestion = questions[index];

    //     if(questions.Contains(currentQuestion)){
    //         questions.Remove(currentQuestion);
    //     }

    // }
    
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

            for(int i = 0; i < answerButton.Length; i++){
                TextMeshProUGUI btnText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
                btnText.text = currentQuestion.GetAnswer(i);
            }
        }
        
    }


    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButton.Length; i++)
        {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    // void SetDefaultButtonSprite()
    // {
    //     Image buttonImage;
    //     for(int i = 0; i < answerButton.Length; i++){

    //         buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
    //         buttonImage.sprite = defaultAnswerSprite;
        
    //     }
    // }

}
