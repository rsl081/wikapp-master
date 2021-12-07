using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class Tutorial : MonoBehaviour
{
    [SerializeField]Button[] dotBtn;
    [SerializeField]GameObject tutorialBtn;
    [SerializeField]TextMeshProUGUI tutsText; 
    [SerializeField]TutorialSO[] textScriptable;
    string currentText = "";
    string storeText = "";
    [SerializeField]float speedText = 0.05f;
    Coroutine lastRoutine = null;

    Transition transition;

    private void Awake() {

        //Info.Instance.deletePlayerAndProgress();
      

    }

    void Start () {
        Init();
	}

    void Init()
    {
        // transition = FindObjectOfType<Transition>();

        // if(Info.Instance.isGameSaved())
        // {
        //     transition.StringSceneToLoad();
        //     transition.LoadNextScene();

        // }else{

        // }

        
        dotBtn[0].interactable = false;
        for(int i = 0; i < dotBtn.Length; i++)
        {
		    Button[] btn = dotBtn[i].GetComponents<Button>();
            int buttonIndex = i;
            dotBtn[i].onClick.AddListener (() => TaskOnClick(buttonIndex));
        }
        currentText = textScriptable[0].GetTuts().ToString();
        lastRoutine = StartCoroutine(WaitNextLetter());
    }

	public void TaskOnClick(int btn)
    {
        StopCoroutine(lastRoutine);
        int lastIndexOfBtn = dotBtn.Length-1;
        int lengthOfBtn = dotBtn.Length;
        int lengthOfScriptable = textScriptable.Length;
        if(lengthOfBtn == lengthOfScriptable){
            for(int i = 0; i < dotBtn.Length; i++)
                {
                    if(i == btn)
                    {
                        dotBtn[i].interactable = false;
                        dotBtn[i].transform.DOPunchPosition(transform.localPosition + 
                                                            new Vector3(0f,-5f,0), 0.5f).Play();
                        ShowTutorialText(i);
                    }else{
                        dotBtn[i].interactable = true;
                    }

                    if(btn == lastIndexOfBtn)
                    {
                        tutorialBtn.SetActive(true);
                    }else{
                        tutorialBtn.SetActive(false);
                    }
            }
        }else{
            Debug.Log("Need same yung length arr ng btn at sciptable");
        }
	}

    private void ShowTutorialText(int index)
    {
        storeText = "";
        currentText = textScriptable[index].GetTuts().ToString();
        lastRoutine = StartCoroutine(WaitNextLetter());
    }

    IEnumerator WaitNextLetter()
    {
        for(int i = 0; i < currentText.Length; i++)
        {
            storeText = storeText + currentText[i];
            tutsText.text = storeText.ToString();
            yield return new WaitForSeconds(speedText);
        }
    }
}
