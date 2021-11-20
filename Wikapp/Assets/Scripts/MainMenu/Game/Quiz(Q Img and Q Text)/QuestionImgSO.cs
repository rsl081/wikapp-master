using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "QuesitonImg", fileName ="Question 1")]
public class QuestionImgSO : ScriptableObject
{
    [SerializeField] Sprite question;
    //[SerializeField] string[] answer = new string[3];
    [SerializeField] string[] answer;
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
    public void SetAnswer(int index, string answer)
    {
        this.answer[index] = answer;
    }
}
