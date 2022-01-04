using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

//! Responsible for UI such as dragImg, text, and quesHolder.
public class QuizQuestionDrag : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] Image questionImageHolder;
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
    [SerializeField] bool isHoldingSomething;
    int index = -1;

    
    AudioSource source;
    [SerializeField] AudioClip correctAnsSound;
    [SerializeField] AudioClip wrongAnsSound;
    [SerializeField] GameObject btnParticleEffect;


    void Awake()
    {
        source = GetComponent<AudioSource>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 1;
        DisplayQuestion();

        
    }

    public void OnAnswerSelected(int index)
    {
        
        StartCoroutine(ShowCorrectAnswer());
        DisplayAnswer(index);
        //GetNextQuestion();

    }

    void DisplayAnswer(int index)
    {
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            source.PlayOneShot(correctAnsSound, 0.7f);
            Image btnText = answerImg[index].GetComponent<Image>();

            questionImage.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-15f,0), 0.5f).Play();

            Instantiate(btnParticleEffect, btnText.transform.position, Quaternion.identity);
            scoreKeeper.IncrementCorrectAnswer();

        }else{
           source.PlayOneShot(wrongAnsSound, 0.7f);

           Image btnText = answerImg[index].GetComponent<Image>();

            questionImage.transform.DOPunchRotation(new Vector3(0f,0f,2f), 0.5f, 10).Play();
            btnText.color = new Color32(244,73,34,255);
            
        }
    
    }

    IEnumerator ShowCorrectAnswer()
    {

        yield return new WaitForSeconds (1f);
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            ShowCompletion();
        }

    
        for(int i = 0; i < answerImg.Length; i++)
        {
            Image button = answerImg[i].GetComponent<Image>();
            button.color = Color.white;

        }

        
        
        GetNextQuestion();
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

            for(int i = 0; i < answerImg.Length; i++){
                Image btnText = answerImg[i].GetComponentInChildren<Image>();
     
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
