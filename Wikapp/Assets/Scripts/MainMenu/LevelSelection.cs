using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public bool isUnlocked = false;
    public Image[] starsImages;//THREE STAR IMAGE
    public Sprite[] starsSprites;
    Transition transition;
    FluidUI fluidUI;
    [SerializeField] GameObject hideThisShit;
    [SerializeField] string _animateStr;

    private void Start()
    {
        transition = FindObjectOfType<Transition>();
        fluidUI = FindObjectOfType<FluidUI>();

        //UpdateLevelButton();
        
        EventCenter.GetInstance().AddEventListener("PressStarButton", UnlockLevel);
        EventCenter.GetInstance().AddEventListener("PressStarButton", UpdateLevelButton);
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UnlockLevel);
        EventCenter.GetInstance().RemoveEventListener("PressStarButton", UpdateLevelButton);
    }

    private void UnlockLevel()
    {
        int previousLvIndex = int.Parse(gameObject.name) - 1;// PlayerPrefs.GetInt("Lv" + gameObject.name) - 1;
        if(PlayerPrefs.GetInt("Lv" + previousLvIndex) > 0)//At least get one stars in previous level
        {
            isUnlocked = true;//can unlock the next level
        }
    }

    private void UpdateLevelButton()
    {
        if(isUnlocked)//MARKER We can play this level
        {
            //lockImage.gameObject.SetActive(false);//we dont want to see the lock image
            for(int i = 0; i < starsImages.Length; i++)
            {
                starsImages[i].gameObject.SetActive(true);
            }

            for(int i = 0; i < PlayerPrefs.GetInt("Lv" + gameObject.name); i++)
            {
                starsImages[i].sprite = starsSprites[i];
            }
        }
        else
        {
            //lockImage.gameObject.SetActive(true);
            for (int i = 0; i < starsImages.Length; i++)
            {
                starsImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void SceneTransition(string _sceneName)
    {

        fluidUI.AnimateUIBtn(_animateStr);

        if(isUnlocked)
        {
            StartCoroutine(DelayTransistion(_sceneName));
        
        }
    }

    IEnumerator DelayTransistion(string _sceneName)
    {
        yield return new WaitForSeconds(0.05f);
        hideThisShit.gameObject.SetActive(false);
        // hideThisShit2.gameObject.SetActive(false);
        transition.sceneName = _sceneName;
        transition.StringSceneToLoad();
    }

}
