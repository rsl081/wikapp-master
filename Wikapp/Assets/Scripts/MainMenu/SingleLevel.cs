using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLevel : MonoBehaviour
{
    private int levelCandiesNum = 0;
    public int levelIndex;
    Transition transition;

    private void Start() {
        transition = FindObjectOfType<Transition>();
    }

    //MARKER This method will be trigger when you press one of the button on the game level
    public void PressCandiesButton(int _candiesNum)
    {
        levelCandiesNum = _candiesNum;

        //MARKER ONLY Your candies number is greater than the record, you can save the new record
        //MARKER PlayerPrefs.GetInt("Lv" + levelIndex) his default value is 0
        if(levelCandiesNum > PlayerPrefs.GetInt("Lv" + levelIndex))//KEY: Lv1; Value: Candies Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, levelCandiesNum);
        }

        EventCenter.GetInstance().EventTrigger("PressStarButton");
        EventCenter.GetInstance().EventTrigger("UpdateMap");

        UIManager.instance.BackToMainMenu();
    }

    

}
