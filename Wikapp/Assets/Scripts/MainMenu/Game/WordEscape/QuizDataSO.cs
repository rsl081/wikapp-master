using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WordEscapeQuestionsData", menuName = "WordQuestionData", order = 1)]
public class QuizDataSO : ScriptableObject
{
    public List<QuestionData> questions;
}
