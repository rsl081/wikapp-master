using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (menuName = "QuesitonTextSO", fileName ="Question 1")]
public class QuestionTextSO : ScriptableObject
{
    [TextArea(10, 14)]
    [SerializeField] string question;
    [SerializeField] string[] answer = new string[3];
    [SerializeField] int correctAnswerIndex;

    public string GetQuestion()
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
