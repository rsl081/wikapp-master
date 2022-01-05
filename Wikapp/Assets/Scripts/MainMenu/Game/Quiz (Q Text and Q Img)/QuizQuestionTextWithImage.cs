using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class QuizQuestionTextWithImage : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] Image questionImageHolder;
     [SerializeField] public Image questionImage;
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
    [SerializeField] bool dragabble;

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
        SetButtonState(false);

    }

    void DisplayAnswer(int index)
    {
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            source.PlayOneShot(correctAnsSound, 0.7f);
            Image button = answerImg[index].GetComponent<Image>();
            button.color = new Color32(34,244,38,255);

            button.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();

            Instantiate(btnParticleEffect, button.transform.position, Quaternion.identity);
            scoreKeeper.IncrementCorrectAnswer();

        }else{
            source.PlayOneShot(wrongAnsSound, 0.7f);
            Image warningBtn = answerImg[index].GetComponent<Image>();
            warningBtn.color = new Color32(244,73,34,255);

            warningBtn.transform.DOPunchRotation(new Vector3(0f,0f,2f), 0.5f, 10).Play();
            

            for(int i = 0; i < answerImg.Length; i++)
            {
                if(i == currentQuestion.GetCorrectAnswerIndex())
                {
                    Image button = answerImg[i].GetComponent<Image>();
                    button.color = new Color32(34,244,38,255);
             
                }
            }
            
        }
    
    }//end of DisplayAnswer

    void ShowCompletion()
    {
        EventCenter.GetInstance().EventTrigger("UpdateScorePercent");
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

            if(isHoldingSomething)
            {
                questionImageHolder.sprite = currentQuestion.GetQuestionHolder();
            }

            if(!dragabble)
            {

                questionText.text = currentQuestion.GetQuestionStr();
                
                for(int i = 0; i < answerImg.Length; i++){
                    TextMeshProUGUI btnText = answerImg[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = currentQuestion.GetAnswer(i);
                }

            }else{

                questionImage.sprite = currentQuestion.GetQuestion();
                questionText.text = currentQuestion.GetQuestionStr();

                for(int i = 0; i < answerImg.Length; i++){

                    TextMeshProUGUI btnText = answerImg[i].GetComponentInChildren<TextMeshProUGUI>();
                    Image imgBtn = answerImg[i].GetComponentInChildren<Image>();
                    
                    imgBtn.gameObject.transform.position = backToSamePosition[i].position;
                    btnText.text = currentQuestion.GetAnswer(i);
                }
            }


        }
        
    }


    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerImg.Length; i++)
        {
            Button button = answerImg[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

}
