using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class QuizText : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionTextSO> questions = new List<QuestionTextSO>();    
    QuestionTextSO currentQuestion;

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
            Image button = answerButton[index].GetComponent<Image>();
            button.color = new Color32(34,244,38,255);

            button.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();

            Instantiate(btnParticleEffect, button.transform.position, Quaternion.identity);
            scoreKeeper.IncrementCorrectAnswer();
            FindObjectOfType<VoiceManager>().StopVoice();

        }else{
            source.PlayOneShot(wrongAnsSound, 0.7f);
            FindObjectOfType<VoiceManager>().StopVoice();

            Image warningBtn = answerButton[index].GetComponent<Image>();
            warningBtn.color = new Color32(244,73,34,255);

            warningBtn.transform.DOPunchRotation(new Vector3(0f,0f,2f), 0.5f, 10).Play();
            

            for(int i = 0; i < answerButton.Length; i++)
            {
                if(i == currentQuestion.GetCorrectAnswerIndex())
                {
                    Image button = answerButton[i].GetComponent<Image>();
                    button.color = new Color32(34,244,38,255);
             
                }
            }
            
        }
    
    }//end of DisplayAnswer

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

    IEnumerator ShowCorrectAnswer()
    {

        yield return new WaitForSeconds (1.5f);
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            ShowCompletion();
        }else{
            FindObjectOfType<VoiceManager>().NextVoice();
        }

        for(int i = 0; i < answerButton.Length; i++)
        {
            
            Image button = answerButton[i].GetComponent<Image>();
            button.color = Color.white;

        }
        
        GetNextQuestion();
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

            questionText.text = currentQuestion.GetQuestion();


            for(int i = 0; i < answerButton.Length; i++){
                //int index = Random.Range(0, agesToChooseFrom.Count);
                //currentQuestion.SetAnswer(i, agesToChooseFrom[i].ToString());
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

}
