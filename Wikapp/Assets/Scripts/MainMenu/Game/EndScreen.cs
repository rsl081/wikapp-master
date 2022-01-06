using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    public Button lockNextImage;//LOCK Next Level IMAGE
    public Image[] starsImages;//THREE STAR IMAGE
    public Sprite[] starsSprites;
    ScoreKeeper scoreKeeper;
    public int levelIndex;
    Transition transition;
    AudioSource source;
    GameObject panelNumber;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        transition = FindObjectOfType<Transition>();

        panelNumber = GameObject.FindGameObjectWithTag("PanelNumber");        
    }
    

    public void ShowFinalScore()
    {
		panelNumber.transform.DOPunchScale(Vector3.one * 0.1f, 0.7f, 5, 0.6f).SetEase(Ease.OutCirc);                                                  
        finalScoreText.text = "Nakakuha ka\nng "
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


        //ShowNextBtn();
    }

    public void ShowCalculatedStar(int _candiesNum)
    {


        if(_candiesNum > PlayerPrefs.GetInt("Lv" + levelIndex))//KEY: Lv1; Value: Candies Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _candiesNum);
        }

    }

    // private void ShowNextBtn()
    // {
        // int previousLvIndex = levelIndex - 1;// PlayerPrefs.GetInt("Lv" + gameObject.name) - 1;
        // Debug.Log(PlayerPrefs.GetInt("Lv" + levelIndex));
        // if(PlayerPrefs.GetInt("Lv" + levelIndex) > 0)//At least get one stars in previous level
        // {
        //     //isUnlocked = true;//can unlock the next level
        //     //! Note Button would appear here
        //     lockNextImage.gameObject.SetActive(true);
        // }else{
        //     lockNextImage.gameObject.SetActive(false);
        // }
    // }

    private void StarFrameGUI()
    {
        // int index = PlayerPrefs.GetInt("Lv" + levelIndex);
        
        for(int i = 0; i < starsImages.Length; i++)
        {
            starsImages[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < scoreKeeper.GetCorrectAnswers(); i++)
        {
            starsImages[i].sprite = starsSprites[i];
        }
  
    }

    public void PressCandiesBackHome()
    {
    
        EventCenter.GetInstance().EventTrigger("PressStarButton");
        EventCenter.GetInstance().EventTrigger("UpdateMap");

        UIManager.instance.BackToMainMenu();
    }


}
