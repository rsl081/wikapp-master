using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

    
    List<string> monthsToChooseFrom = new List<string>(new string[]{
        "Marso","Abril","Mayo"
        });

    //Todo make this year connected to the dropdown in InfGui
    List<string> yearsToChooseFrom = new List<string>(new string[]{
        "2016","2017","2019"});

    List<int> daysToChooseFrom = new List<int>(new int[]{
        28,7,1
    });

    InfoGUI infoGUI;
    public Player currentPlayer = new Player();

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
      
        int agePlayer = int.Parse(Info.Instance.getPlayer()._age);

        string monthPlayer = Info.Instance.getPlayer()._month;
        int dayPlayer = Info.Instance.getPlayer()._day;
        string yearPlayer = Info.Instance.getPlayer()._year;

        bool ageIsAlreadyExist = agesToChooseFrom.Contains(agePlayer);

        bool monthIsAlreadyExist = monthsToChooseFrom.Contains(monthPlayer);
        bool dayIsAlreadyExist = daysToChooseFrom.Contains(dayPlayer);
        bool yearIsAlreadyExist = yearsToChooseFrom.Contains(yearPlayer);

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

        if(monthIsAlreadyExist){

            monthsToChooseFrom.Remove(monthPlayer);
            monthsToChooseFrom.Add(monthPlayer);

        }else{
            monthsToChooseFrom.Remove("Marso");


            monthsToChooseFrom.Add(monthPlayer);

        }

        if(dayIsAlreadyExist){

            daysToChooseFrom.Remove(dayPlayer);
            daysToChooseFrom.Add(dayPlayer);

        }else{
            daysToChooseFrom.Remove(28);


            daysToChooseFrom.Add(dayPlayer);

        }

        if(yearIsAlreadyExist){

            yearsToChooseFrom.Remove(yearPlayer);
            yearsToChooseFrom.Add(yearPlayer);

        }else{
            yearsToChooseFrom.Remove("2016");

            yearsToChooseFrom.Add(yearPlayer);

        }

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
        if(answerButton[index].GetComponentInChildren<TextMeshProUGUI>().text.Equals(Info.Instance.getPlayer()._age) ||
            answerButton[index].GetComponentInChildren<TextMeshProUGUI>().text.Equals(Info.Instance.getPlayer()._month) ||
            answerButton[index].GetComponentInChildren<TextMeshProUGUI>().text.Equals(Info.Instance.getPlayer()._day.ToString()) ||
            answerButton[index].GetComponentInChildren<TextMeshProUGUI>().text.Equals(Info.Instance.getPlayer()._year)){
            source.PlayOneShot(correctAnsSound, 0.7f);
            Image button = answerButton[index].GetComponent<Image>();
            button.color = new Color32(34,244,38,255);

            button.transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();

            Instantiate(btnParticleEffect, button.transform.position, Quaternion.identity);
            scoreKeeper.IncrementCorrectAnswer();

        }else{

          source.PlayOneShot(wrongAnsSound, 0.7f);
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
    

    }

    IEnumerator ShowCorrectAnswer()
    {

        yield return new WaitForSeconds (1.5f);
        if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            ShowCompletion();
        }

        for(int i = 0; i < answerButton.Length; i++)
        {
            Image button = answerButton[i].GetComponent<Image>();
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

            if(index == 0)
            {
                currentQuestion = questions[index];

                questionText.text = currentQuestion.GetQuestion();


                for(int i = 0; i < answerButton.Length; i++){
                    //int index = Random.Range(0, agesToChooseFrom.Count);
                    currentQuestion.SetAnswer(i, agesToChooseFrom[i].ToString());
                    TextMeshProUGUI btnText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = currentQuestion.GetAnswer(i);
                }
            }else if(index == 1)
            {
                currentQuestion = questions[index];

                questionText.text = currentQuestion.GetQuestion();


                for(int i = 0; i < answerButton.Length; i++){
                    //int index = Random.Range(0, agesToChooseFrom.Count);
                    currentQuestion.SetAnswer(i, monthsToChooseFrom[i].ToString());
                    TextMeshProUGUI btnText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = currentQuestion.GetAnswer(i);
                }
            }else if(index == 2)
            {
                currentQuestion = questions[index];

                questionText.text = currentQuestion.GetQuestion();


                for(int i = 0; i < answerButton.Length; i++){
                    //int index = Random.Range(0, agesToChooseFrom.Count);
                    currentQuestion.SetAnswer(i, daysToChooseFrom[i].ToString());
                    TextMeshProUGUI btnText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = currentQuestion.GetAnswer(i);
                }
            }else if(index == 3)
            {
                currentQuestion = questions[index];

                questionText.text = currentQuestion.GetQuestion();


                for(int i = 0; i < answerButton.Length; i++){
                    //int index = Random.Range(0, agesToChooseFrom.Count);
                    currentQuestion.SetAnswer(i, yearsToChooseFrom[i].ToString());
                    TextMeshProUGUI btnText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
                    btnText.text = currentQuestion.GetAnswer(i);
                }
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
