using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QDragWithText : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] Image questionImageHolder;
    [SerializeField] public Image questionImage;
    [SerializeField] List<QDragWithTextSO> questions = new List<QDragWithTextSO>();    
    QDragWithTextSO currentQuestion;
    [SerializeField] public Transform[] backToSamePosition;

    [Header("Answers")]
    [SerializeField] public GameObject[] answerText;
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
    [Header ("Audio")]
    AudioSource source;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        source = FindObjectOfType<AudioSource>();
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
            if(isHoldingSomething)
            {
                questionImageHolder.sprite = currentQuestion.GetQuestionHolder();
            }

            for(int i = 0; i < answerText.Length; i++){
                TextMeshProUGUI btnText = answerText[i].GetComponentInChildren<TextMeshProUGUI>();
                Image imgBtn = answerText[i].GetComponentInChildren<Image>();
                
                imgBtn.gameObject.transform.position = backToSamePosition[i].position;
                btnText.text = currentQuestion.GetAnswer(i);
            }
        }
        
    }

    public void PlaySound(int _index)
    {
        source.clip = currentQuestion.GetSourceAudio()[_index];
        source.Play(0);
    }
    public void PauseSound(int _index)
    {
        source.clip = currentQuestion.GetSourceAudio()[_index];
        source.Pause();
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
