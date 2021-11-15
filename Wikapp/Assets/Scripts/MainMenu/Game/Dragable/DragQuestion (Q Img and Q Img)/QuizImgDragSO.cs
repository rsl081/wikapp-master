using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "QuesitonImgToImgDragable", fileName ="Question 1")]
public class QuizImgDragSO : ScriptableObject
{
    [SerializeField] Sprite question;
    [SerializeField] Sprite[] answer = new Sprite[3];
    [SerializeField] int correctAnswerIndex;

    public Sprite GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }
    public Sprite GetAnswer(int index)
    {
        return answer[index];
    }
}
