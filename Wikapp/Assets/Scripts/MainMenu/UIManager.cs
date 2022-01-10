using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
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
    [SerializeField] GameObject myCanvas;
    
	private List<Sequence> animationSequences = new List<Sequence>();

	private const float INITIAL_DELAY = 1f;
	private const float DELAY_BETWEEN_BUTTONS = 0.3f;

    Tweener shakeTween;
    TweenCallback shakeTweenComplete;

    Transition transition;
    FluidUI fluidUI;

    AudioSource audioSource;
    [SerializeField] AudioClip impactError;
    [SerializeField] AudioClip impactPop;
    [SerializeField] AudioClip poppingSound;
    AudioManager audioManager;

    private void Awake() {
        if(instance == null)
        {
            instance = this; 
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        AnimatePanels();
        
    }


    void Start()
    {
        init();
    }

    void init()
    {
        transition = FindObjectOfType<Transition>();
        fluidUI = FindObjectOfType<FluidUI>();

        UpdateNameUI();
        UpdateLockedStarUI();
        UpdateUnLockedStarUI();

        EventCenter.GetInstance().EventTrigger("UpdateMap");
        EventCenter.GetInstance().EventTrigger("DestroyMyAudioManager");
        //EventCenter.GetInstance().EventTrigger("DestroyGameMusic");

        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateNameUI);
        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateLockedStarUI);
        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateUnLockedStarUI);

    }
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateNameUI);
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateLockedStarUI);
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateUnLockedStarUI);
     
    }

    //Update OUR Candies UI on the top left corner
    private void UpdateNameUI()
    {
        candies = Levels();

        candyText.text = $"Kamusta,\n{Info.Instance.getPlayer()._name}";
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
 

    private void UpdateLockedStarUI()
    {
        
        for(int i = 0; i < mapSelections.Length; i++)
        {
            //questStarsTexts[i].text = mapSelections[i].questNum.ToString();
            if (mapSelections[i].isUnlock == false)//If one of the Map is locked
            {
                
                lockedStarsTexts[i].text = (mapSelections[i].endLevel - 24).ToString();
                
            }
        }
    }

    private void UpdateUnLockedStarUI()
    {
        
        for(int i = 0; i < mapSelections.Length; i++)
        {
            //Number of candies to unlock
           // unlockStarsTexts[i].text = candies.ToString() + "/" + mapSelections[i].endLevel * 1;
            unlockStarsTexts[i].text = candies.ToString();
            
        }

    }

    //MARKER This method will be triggered when we press the (FOUR) level panel button
    public void PressMapButton(int _mapIndex)
    {
        string gameObjectName = mapSelections[_mapIndex].gameObject.name;
        if(mapSelections[_mapIndex].isUnlock == true)//You can open this level panel
        {
            audioSource.PlayOneShot(impactPop, 0.7f);
            fluidUI.AnimateUIBtn(gameObjectName);

        }
        else
        {
            mapSelections[_mapIndex].transform.DOShakePosition(3,3); //Shake if the card is lock
            audioSource.PlayOneShot(impactError, 0.7f);
            //Debug.Log("You cannot open this scene now. Please work hard to collect more candies");
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
        //SceneManager.LoadScene("MainMenu");
    }
    public void BackToMainMenu()
    {
        mapSelectionPanel.gameObject.SetActive(true);
        myCanvas.SetActive(true);
        for (int i = 0; i < mapSelections.Length; i++)
        {
            levelSelectionPanels[i].gameObject.SetActive(true);
        }
        
        SceneManager.LoadScene("MainMenu");
        fluidUI.AnimateUIBtn("BackToMain");
    }

    public void Cheat(){
        candies+=24;
        EventCenter.GetInstance().EventTrigger("UpdateMap");
        UpdateLockedStarUI();
    }

    #region Animate Panels XD
    int x = 0;
    private void AnimatePanels()
	{
        
        
		for (int i = 0; i < 4; i++)
		{
			mapSelections[i].transform.localScale = Vector3.zero;
			AnimatePanel(i, INITIAL_DELAY + DELAY_BETWEEN_BUTTONS * i);
            StartCoroutine(SoundEffect(INITIAL_DELAY + DELAY_BETWEEN_BUTTONS * i));
		}


	}
   
    IEnumerator SoundEffect(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(x <= 1)
        {
            audioSource.PlayOneShot(poppingSound, 0.7f);
            StartCoroutine(SoundEffect(delay));
        }
        x+=1;
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
        

		seq.Append(button.transform.DOScale(1.58f, 0.1f));
		seq.Append(button.transform.DOPunchScale(Vector3.one * 0.1f, 0.7f, 5, 0.6f).SetEase(Ease.OutCirc));
		seq.PrependInterval(delay);
        
        
	}
    #endregion


}
