using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    
    public GameObject mapSelectionPanel;
    public GameObject[] levelSelectionPanels;

    [Header ("Our Candy UI")]
    public int candies;
    public TextMeshProUGUI candyText;
    //public TextMeshProUGUI[] candyLevelText;
    public MapSelection[] mapSelections;
    public TextMeshProUGUI[] questStarsTexts;
    public TextMeshProUGUI[] lockedStarsTexts;
    public TextMeshProUGUI[] unlockStarsTexts;
    
	private List<Sequence> animationSequences = new List<Sequence>();

	private const float INITIAL_DELAY = 1f;
	private const float DELAY_BETWEEN_BUTTONS = 0.3f;

    Tweener shakeTween;
    TweenCallback shakeTweenComplete;

    Transition transition;
    FluidUI fluidUI;

    protected override void Awake()
    {
        base.Awake();
        AnimatePanels();
    }


    void Start()
    {
        Info.Instance.deletePlayerAndProgress();
        //PlayerPrefs.DeleteAll();

        init();
    }

    void init()
    {
        transition = FindObjectOfType<Transition>();
        fluidUI = FindObjectOfType<FluidUI>();

        UpdateStarUI();
        UpdateLockedStarUI();
        UpdateUnLockedStarUI();

        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateStarUI);
        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateLockedStarUI);
        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateUnLockedStarUI);
    }
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateStarUI);
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateLockedStarUI);
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateUnLockedStarUI);
    }

    //Update OUR Candies UI on the top left corner
    private void UpdateStarUI()
    {
        candies = Levels();
        candyText.text = candies.ToString();
    }

    
    public int Levels()
    {
        int num = 0;

        for(int i = 1; i <= 96; i++){
            num = num + PlayerPrefs.GetInt("Lv" + i);
        }

        return num;
    }

    private int KinderGartenLevel()
    {
        int num = 0;

        for(int i = 1; i <= 24; i++){
            num = num + PlayerPrefs.GetInt("Lv" + i);
        }

        return num;
    }

    private int FirstGrade()
    {
        int num = 0;

        for(int i = 25; i <= 48; i++){
            num = num + PlayerPrefs.GetInt("Lv" + i);
        }

        return num;
    }
    private int SecondGrade()
    {
        int num = 0;

        for(int i = 49; i <= 72; i++){
            num = num + PlayerPrefs.GetInt("Lv" + i);
        }

        return num;
    }
    private int ThirdGrade()
    {
        int num = 0;

        for(int i = 73; i <= 96; i++)
        {
            num = num + PlayerPrefs.GetInt("Lv" + i);
        }

        return num;
    }

    private void UpdateLockedStarUI()
    {
        
        for(int i = 0; i < mapSelections.Length; i++)
        {
            //questStarsTexts[i].text = mapSelections[i].questNum.ToString();
            if (mapSelections[i].isUnlock == false)//If one of the Map is locked
            {
                lockedStarsTexts[i].text = (mapSelections[i].endLevel * 1).ToString();
                
                
            }
        }
    }

    private void UpdateUnLockedStarUI()
    {
        
        for(int i = 0; i < mapSelections.Length; i++)
        {
            //Number of candies to unlock
            unlockStarsTexts[i].text = candies.ToString() + "/" + mapSelections[i].endLevel * 1;
            
        //     switch(i)
        //     {
        //         case 0://Kinder
        //             unlockStarsTexts[i].text = (KinderGartenLevel()) + "/" +
        //                 (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 1;

        //             break;
        //         case 1://1st
        //             unlockStarsTexts[i].text = (FirstGrade()) + "/" +
        //                 (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 1;

        //             break;
        //         case 2://2nd
        //             unlockStarsTexts[i].text = (SecondGrade()) + "/" +
        //                 (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 1;
        //             break;
        //         case 3://3rd
        //             unlockStarsTexts[i].text = (ThirdGrade()) + "/" +
        //                 (mapSelections[i].endLevel - mapSelections[i].startLevel + 1) * 1;
        //             break;
        //     }
        }
    }

    //MARKER This method will be triggered when we press the (FOUR) level panel button
    public void PressMapButton(int _mapIndex)
    {
        string gameObjectName = mapSelections[_mapIndex].gameObject.name;
        if(mapSelections[_mapIndex].isUnlock == true)//You can open this level panel
        {
            // levelSelectionPanels[_mapIndex].gameObject.SetActive(true);
            // mapSelectionPanel.gameObject.SetActive(false);
            fluidUI.AnimateUIBtn(gameObjectName);
        }
        else
        {
            mapSelections[_mapIndex].transform.DOShakePosition(3,3); //Shake if the card is lock
            Debug.Log("You cannot open this scene now. Please work hard to collect more candies");
        }
    }


    public void BackButton()
    {
        mapSelectionPanel.gameObject.SetActive(true);
        for(int i = 0; i < mapSelections.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
    }

    //MARKER this method will be call on the SingleLevel button event
    public void BackMapSelection()
    {
        mapSelectionPanel.gameObject.SetActive(true);
        for (int i = 0; i < mapSelections.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(false);
        }
        transition.StringSceneToLoad();
        //SceneManager.LoadScene("LevelSelection");
    }

    public void Cheat(){
        candies+=24;
        EventCenter.GetInstance().EventTrigger("UpdateMap");
        UpdateLockedStarUI();
    }

    #region Animate Panels XD
    private void AnimatePanels()
	{
		for (int i = 0; i < 4; i++)
		{
			mapSelections[i].transform.localScale = Vector3.zero;
			AnimatePanel(i, INITIAL_DELAY + DELAY_BETWEEN_BUTTONS * i);
		}
	}

    private void AnimatePanel(int index, float delay)
	{
		if (animationSequences.Count <= index)
		{
			animationSequences.Add(DOTween.Sequence());
		}
		else
		{
			if (animationSequences[index].IsPlaying())
			{
				animationSequences[index].Kill(true);
			}
		}

		var seq = animationSequences[index];
		var button = mapSelections[index];

		seq.Append(button.transform.DOScale(1.3f, 0.1f));
		seq.Append(button.transform.DOPunchScale(Vector3.one * 0.5f, 0.7f, 5, 0.6f).SetEase(Ease.OutCirc));
		seq.PrependInterval(delay);
	}
    #endregion
}
