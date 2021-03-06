using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "QDragWithTextSO", fileName ="QDragWithTextSO 1")]
public class QDragWithTextSO : ScriptableObject
{
    [SerializeField] AudioClip[] source;
    [SerializeField] Sprite question;
    [SerializeField] Sprite questionWithHolder;
    [SerializeField] string questionStr;
    [SerializeField] string[] answer = new string[3];
    [SerializeField] Sprite[] answerSprite = new Sprite[3];
    [SerializeField] int correctAnswerIndex;

    public Sprite GetQuestion()
    {
        return question;
    }

    public string GetQuestionStr()
    {
        return questionStr;
    }

    public AudioClip[] GetSourceAudio()
    {
        return source;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public string GetAnswer(int index)
    {
        return answer[index];
    }

    public Sprite GetAnswerImg(int index)
    {
        return answerSprite[index];
    }
    public Sprite GetQuestionHolder()
    {
        return questionWithHolder;
    }
}
