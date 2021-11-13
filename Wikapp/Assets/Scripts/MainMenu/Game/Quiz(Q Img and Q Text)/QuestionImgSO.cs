using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "QuesitonImg", fileName ="Question 1")]
public class QuestionImgSO : ScriptableObject
{
    [SerializeField] Sprite question;
    [SerializeField] string[] answer = new string[3];
    [SerializeField] int correctAnswerIndex;

    public Sprite GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public string GetAnswer(int index)
    {
        return answer[index];
    }
}
