using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "QuesitonTracingNumber", fileName ="Question 1")]
public class TracingNumberSO : ScriptableObject
{
    [SerializeField] GameObject question;

    public GameObject GetQuestion()
    {
        return question;
    }
    // public bool GetCorrectAnswerBool()
    // {
    //     return correctAnswer;
    // }
    // public string GetAnswer(int index)
    // {
    //     return answer[index];
    // }
    // public void SetAnswer(int index, string answer)
    // {
    //     this.answer[index] = answer;
    // }
}
