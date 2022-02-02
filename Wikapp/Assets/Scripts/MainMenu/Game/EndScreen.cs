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
    GameObject unlockGrade1;
    int indexStar;

    // Start is called before the first frame update
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        transition = FindObjectOfType<Transition>();

        panelNumber = GameObject.FindGameObjectWithTag("PanelNumber");        
        unlockGrade1 = GameObject.FindGameObjectWithTag("UnlockGrade1");
        unlockGrade1.SetActive(false);
    }

    public void ShowFinalScore()
    {

     
		panelNumber.transform.DOPunchScale(Vector3.one * 0.1f, 0.7f, 5, 0.6f).SetEase(Ease.OutCirc);                                                  
        finalScoreText.text = "Nakakuha ka\nng "
            + scoreKeeper.CalculateScore() + "%";


    }

    private void CountCandy(ICandyScore icandy)
    {
        //Debug.Log(icandy.Levels());
        EventCenter.GetInstance().EventTrigger("PressStarButton");
        EventCenter.GetInstance().EventTrigger("UpdateMap");

        if((icandy.Levels() >= 24 && icandy.Levels() <= 27) && PlayerPrefs.GetInt("isOpenOnce") == 1)
        {

            unlockGrade1.SetActive(true);
            PlayerPrefs.SetInt("isOpenOnce",0);
            PlayerPrefs.Save();

        }

    }

    public void ShowStarGUI()
    {

        if(scoreKeeper.CalculateScore() >= 90){ 

            ShowCalculatedStar(3);
            indexStar = 3;
            StarFrameGUI();
        }else if(scoreKeeper.CalculateScore() >= 50){
            
            ShowCalculatedStar(2);
            indexStar = 2;
            StarFrameGUI();

        }else if(scoreKeeper.CalculateScore() >= 30){
            
            ShowCalculatedStar(1);
            indexStar = 1;
            StarFrameGUI();

        }else{

            ShowCalculatedStar(0);
            StarFrameGUI();
        }
      
        CountCandy(new UIManager());

        //ShowNextBtn();
    }

    public void ShowCalculatedStar(int _candiesNum)
    {


        if(_candiesNum > PlayerPrefs.GetInt("Lv" + levelIndex))//KEY: Lv1; Value: Candies Number
        {
            PlayerPrefs.SetInt("Lv" + levelIndex, _candiesNum);
        }

    }


    private void StarFrameGUI()
    {
        // int index = PlayerPrefs.GetInt("Lv" + levelIndex);
        
        for(int i = 0; i < starsImages.Length; i++)
        {
            starsImages[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < indexStar; i++)
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
