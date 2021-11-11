using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextTutorial")]
public class TutorialSO : ScriptableObject
{
    [TextArea(10,14)][SerializeField] string tutsText;
    public string GetTuts()
    {
        return tutsText;
    }
}
