using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    public Button lockNextImage;//LOCK Next Level IMAGE
    public Image[] starsImages;//THREE STAR IMAGE
    public Sprite[] starsSprites;
    ScoreKeeper scoreKeeper;
    public int levelIndex;
    Transition transition;
    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        transition = FindObjectOfType<Transition>();
    }

    public void ShowFinalScore()
    {
        finalScoreText.text = "You got a\n score of "
            + scoreKeeper.CalculateScore() + "%";
    }

    public void ShowStarGUI()
    {
        
        
        if(scoreKeeper.CalculateScore() >= 90){ 

            ShowCalculatedStar(3);
            StarFrameGUI();
        }else if(scoreKeeper.CalculateScore() >= 50){
            
            ShowCalculatedStar(2);
            StarFrameGUI();

        }else if(scoreKeeper.CalculateScore() >= 30){
            
            ShowCalculatedStar(1);
            StarFrameGUI();

        }else{

            ShowCalculatedStar(0);
            StarFrameGUI();
        }
        ShowNextBtn();
    }

    public void ShowCalculatedStar(int _candiesNum)
    {


        if(_candiesNum > PlayerPrefs.GetInt("Lv" + levelIndex))//KEY: Lv1; Value: Candies Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _candiesNum);
        }


    }

    private void ShowNextBtn()
    {
        int previousLvIndex = levelIndex - 1;// PlayerPrefs.GetInt("Lv" + gameObject.name) - 1;
        Debug.Log(previousLvIndex);
        if(PlayerPrefs.GetInt("Lv" + levelIndex) > 0)//At least get one stars in previous level
        {
            //isUnlocked = true;//can unlock the next level
            //! Note Button would appear here
            lockNextImage.gameObject.SetActive(true);
        }
    }

    private void StarFrameGUI()
    {
        int index = PlayerPrefs.GetInt("Lv" + levelIndex);
        //lockImage.gameObject.SetActive(false);//we dont want to see the lock image
        for(int i = 0; i < starsImages.Length; i++)
        {
            starsImages[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < index; i++)
        {
            starsImages[i].sprite = starsSprites[i];
        }
        // for(int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
        // {
        //     starsImages[i].sprite = starsSprites[i];
        // }
    }

    public void PressCandiesBackHome()
    {
    
        EventCenter.GetInstance().EventTrigger("PressStarButton");
        EventCenter.GetInstance().EventTrigger("UpdateMap");

        UIManager.instance.BackToMainMenu();
    }


}
