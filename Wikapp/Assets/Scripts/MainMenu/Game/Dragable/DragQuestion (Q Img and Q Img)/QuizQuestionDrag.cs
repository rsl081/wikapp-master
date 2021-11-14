using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//! Responsible for UI such as dragImg, text, and quesHolder.
public class QuizQuestionDrag : MonoBehaviour
{
    [Header("Question")]
    [SerializeField] Image questionImage;
    
    //!Responsible for getting qustion
    // [SerializeField] List<QuestionImgSO> questions = new List<QuestionImgSO>();    
    // QuestionImgSO currentQuestion;

    
    [Header("Answers")]
    [SerializeField] GameObject[] answerButton;
    
    //! get the gameObjectName
    int correctAnswerIndex;

    [Header("Scoring")]
    ScoreKeeper scoreKeeper;

    [Header ("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        //! progressBar.maxValue = questions.Count;
        progressBar.value = 1;
        //! DisplayQuestion();
    }

    
}
