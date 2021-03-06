using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    [SerializeField] AudioClip correctAnsSound;
    [SerializeField] AudioClip wrongAnsSound;
    [SerializeField] GameObject btnParticleEffect;
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
        
        
        StartCoroutine(ShowCorrectAnswer());
        DisplayAnswer(index);
        SetButtonState(false);
   
     

    }

    void DisplayAnswer(int index)
    {
        if(index == currentQuestion.GetCorrectAnswerIndex()){
            source.PlayOneShot(correctAnsSound, 0.7f);
            Image button = answerText[index].GetComponent<Image>();
            button.color = new Color32(34,244,38,255);

            button.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();

            Instantiate(btnParticleEffect, button.transform.position, Quaternion.identity);
            scoreKeeper.IncrementCorrectAnswer();

        }else{
            source.PlayOneShot(wrongAnsSound, 0.7f);
            Image warningBtn = answerText[index].GetComponent<Image>();
            warningBtn.color = new Color32(244,73,34,255);

            warningBtn.transform.DOPunchRotation(new Vector3(0f,0f,2f), 0.5f, 10).Play();
            

            for(int i = 0; i < answerText.Length; i++)
            {
                if(i == currentQuestion.GetCorrectAnswerIndex())
                {
                    Image button = answerText[i].GetComponent<Image>();
                    button.color = new Color32(34,244,38,255);
             
                }
            }
            
        }
    
    }

    
    IEnumerator ShowCorrectAnswer()
    {

        yield return new WaitForSeconds (1.5f);
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            ShowCompletion();
        }

        for(int i = 0; i < answerText.Length; i++)
        {
            Image button = answerText[i].GetComponent<Image>();
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
        source.Pause();
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
        source.loop = true;

    }
    public void PauseSound(int _index)
    {

        source.clip = currentQuestion.GetSourceAudio()[_index];
        source.Pause();
        source.loop = false;
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerText.Length; i++)
        {
            Button button = answerText[i].GetComponent<Button>();
            button.interactable = state;
        }
    }


}
