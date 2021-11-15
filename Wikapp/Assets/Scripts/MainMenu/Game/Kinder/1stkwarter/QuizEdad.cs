using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizEdad : MonoBehaviour
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

    List<int> agesToChooseFrom = new List<int>(new int[]{3,4,5});
    InfoGUI infoGUI;
    public Player currentPlayer = new Player();

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 1;

        int agePlayer = int.Parse(Info.Instance.getPlayer()._age);
        Debug.Log(agePlayer);
        bool ageIsAlreadyExist = agesToChooseFrom.Contains(agePlayer);

        //if age is already exist
        if(ageIsAlreadyExist){
            agesToChooseFrom.Remove(agePlayer);
            //The data was remove, the list now became 2 digit  numbersToChooseFrom [num1, num2]
            //This is the reason why I add two digits
            agesToChooseFrom.Add(agePlayer);

        }else{
            agesToChooseFrom.Remove(3);
            agesToChooseFrom.Add(agePlayer);
        }

        DisplayQuestion();

     
    }

    private void Start() {
     
        
    }

    public void OnAnswerSelected(int index)
    {
        DisplayAnswer(index);
        SetButtonState(false);
        GetNextQuestion();

    }

    void DisplayAnswer(int index)
    {
    
        if(answerButton[index].GetComponentInChildren<TextMeshProUGUI>().text.Equals(Info.Instance.getPlayer()._age)){
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
                currentQuestion.SetAnswer(i, agesToChooseFrom[i].ToString());
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
