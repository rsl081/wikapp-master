using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;

public class WordEscapeGameManager : MonoBehaviour
{
    //This code came from MadFireOn
    public static WordEscapeGameManager instance; //Instance to make is available in other scripts without reference
    //[SerializeField] private GameObject gameComplete;
    [SerializeField] private GameObject quizCanvas;
    [SerializeField] private Slider gameSlider;
    //Scriptable data which store our questions data
    [SerializeField] private QuizDataSO questionDataScriptable;
    [SerializeField] private Image questionImage;           //image element to show the image
    [SerializeField] private WordData[] answerWordList;     //list of answers word in the game
    [SerializeField] private WordData[] optionsWordList;    //list of options word in the game
    private GameStatus gameStatus = GameStatus.Playing;     //to keep track of game status
    private char[] wordsArray = new char[12];               //array which store char of each options
    private List<int> selectedWordsIndex;                   //list which keep track of option word index w.r.t answer word index
    private int currentAnswerIndex = 0, currentQuestionIndex = 0;   //index to keep track of current answer and current question
    private bool correctAnswer = true;                      //bool to decide if answer is correct or not
    private string answerWord;                              //string to store answer of current question

    public bool isComplete;
    private ScoreKeeper scoreKeeper;
    AudioSource source;
    [SerializeField] AudioClip correctBtn;
    [SerializeField] AudioClip letterSound;
    [SerializeField] AudioClip backSound;
    [SerializeField] GameObject awesomeVfx;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        source = GetComponent<AudioSource>();

        gameSlider.maxValue = questionDataScriptable.questions.Count;
        gameSlider.value = 1; //set slider value to 1
     
        selectedWordsIndex = new List<int>();           //create a new list at start
        SetQuestion();                                  //set question
    }

    void SetQuestion()
    {
        
   
        gameStatus = GameStatus.Playing;                //set GameStatus to playing 

        //set the answerWord string variable
        answerWord = questionDataScriptable.questions[currentQuestionIndex].answer;
        //set the image of question
        questionImage.sprite = questionDataScriptable.questions[currentQuestionIndex].questionImage;
            
        ResetQuestion();                               //reset the answers and options value to orignal     

        selectedWordsIndex.Clear();                     //clear the list for new question
        Array.Clear(wordsArray, 0, wordsArray.Length);  //clear the array
        
        //add the correct char to the wordsArray
        for (int i = 0; i < answerWord.Length; i++)
        {
            wordsArray[i] = char.ToUpper(answerWord[i]);
        }

        //add the dummy char to wordsArray
        for (int j = answerWord.Length; j < wordsArray.Length; j++)
        {
            wordsArray[j] = (char)UnityEngine.Random.Range(65, 90);

        }


        wordsArray = ShuffleList.ShuffleListItems<char>(wordsArray.ToList()).ToArray(); //Randomly Shuffle the words array

        //set the options words Text value
        for (int k = 0; k < optionsWordList.Length; k++)
        {
            optionsWordList[k].SetWord(wordsArray[k]);
        }

    }

    //Method called on Reset Button click and on new question
    public void ResetQuestion()
    {
        //activate all the answerWordList gameobject and set their word to " "
        for (int i = 0; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(true);
            answerWordList[i].SetWord(' ');
        }

        //Now deactivate the unwanted answerWordList gameobject (object more than answer string length)
        for (int i = answerWord.Length; i < answerWordList.Length; i++)
        {
            answerWordList[i].gameObject.SetActive(false);
        }

        //activate all the optionsWordList objects
        for (int i = 0; i < optionsWordList.Length; i++)
        {
            optionsWordList[i].gameObject.SetActive(true);
        }

        currentAnswerIndex = 0;
    }

    /// <summary>
    /// When we click on any options button this method is called
    /// </summary>
    /// <param name="value"></param>
    public void SelectedOption(WordData value)
    {
        source.PlayOneShot(letterSound, 0.7f);

        //if gameStatus is next or currentAnswerIndex is more or equal to answerWord length
        if (gameStatus == GameStatus.Next || currentAnswerIndex >= answerWord.Length) return;

        selectedWordsIndex.Add(value.transform.GetSiblingIndex()); //add the child index to selectedWordsIndex list
        value.gameObject.SetActive(false); //deactivate options object
        answerWordList[currentAnswerIndex].SetWord(value.wordValue); //set the answer word list
        Instantiate(awesomeVfx, answerWordList[currentAnswerIndex].transform.position, Quaternion.identity);

        currentAnswerIndex++;   //increase currentAnswerIndex

        //if currentAnswerIndex is equal to answerWord length
        if (currentAnswerIndex == answerWord.Length)
        {
            correctAnswer = true;   //default value
            //loop through answerWordList
            for (int i = 0; i < answerWord.Length; i++)
            {
                //if answerWord[i] is not same as answerWordList[i].wordValue
                if (char.ToUpper(answerWord[i]) != char.ToUpper(answerWordList[i].wordValue))
                {
                    correctAnswer = false; //set it false
                    break; //and break from the loop
                }
            }

 

            //if correctAnswer is true
            if (correctAnswer)
            {
                //Debug.Log("Correct Answer");
                gameStatus = GameStatus.Next; //set the game status
                currentQuestionIndex++; //increase currentQuestionIndex
                gameSlider.value++;//increase slider value to one
                scoreKeeper.IncrementCorrectAnswer();//increment correct answer
                scoreKeeper.IncrementQuesitonsSeen();

                //Debug.Log(gameSlider.value++);
                
                //if currentQuestionIndex is less that total available questions
                if (currentQuestionIndex < questionDataScriptable.questions.Count)
                {
                    source.PlayOneShot(correctBtn, 0.7f);
                    //loop through answerWordList
                    for (int i = 0; i < answerWord.Length; i++)
                    {
                        answerWordList[i].transform.DOPunchPosition(transform.localPosition + 
                                    new Vector3(0f,-5f,0), 0.5f).Play();
                    }
                    
                    Invoke("SetQuestion", 1f); //go to next question
                }
                else
                {
                    scoreKeeper.IncrementCorrectAnswer();

                    source.PlayOneShot(correctBtn, 0.7f);
                  
                    for (int i = 0; i < answerWord.Length; i++)
                    {
                        answerWordList[i].transform.DOPunchPosition(transform.localPosition + 
                                    new Vector3(0f,-5f,0), 0.5f).Play();
                    }
                  
                    Invoke(nameof(ShowCompletion), 1f);
                }
            }
        }
    }

    void ShowCompletion()
    {
        quizCanvas.SetActive(false);
        isComplete = true;
        EventCenter.GetInstance().EventTrigger("UpdateScorePercent");
    }


    public void ResetLastWord()
    {

        if (selectedWordsIndex.Count > 0)
        {
            source.PlayOneShot(backSound, 0.7f);
            int index = selectedWordsIndex[selectedWordsIndex.Count - 1];
            optionsWordList[index].gameObject.SetActive(true);
            selectedWordsIndex.RemoveAt(selectedWordsIndex.Count - 1);

            currentAnswerIndex--;
            answerWordList[currentAnswerIndex].SetWord(' ');
        }
    }

}

[System.Serializable]
public class QuestionData
{
    public Sprite questionImage;
    public string answer;
}

public enum GameStatus
{
   Next,
   Playing
}

