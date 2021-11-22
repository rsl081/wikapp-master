using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintBucket : MonoBehaviour
{
    public Color[] colorList;
    public Color curColor;
    public int colorCount;
    bool isLocked = false;
    bool isFirstClicked = false;
    public bool isComplete;

    [SerializeField] Slider progressBar;

    SpriteRenderer[] boyColoring;
    SpriteRenderer[] girlColoring;
    ScoreKeeper scoreKeeper;

    private void Start() {
        boyColoring = FindObjectOfType<BoyColoring>().GetComponentsInChildren<SpriteRenderer>();
        girlColoring = FindObjectOfType<GirlColoring>().GetComponentsInChildren<SpriteRenderer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();

        progressBar.maxValue = (boyColoring.Length + girlColoring.Length) - 1;
        progressBar.value = 1;

    }

    private void OnDestroy() {
        
    }

    // Update is called once per frame
    void Update()
    {
        // curColor = colorList[colorCount];

        // if(Input.touchCount > 0){

        //     Touch touch = Input.GetTouch(0);
        //     Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

        //     //var ray = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //     RaycastHit2D hit  = Physics2D.Raycast(touchPos, -Vector2.up);

        //     switch(touch.phase){
        //         case TouchPhase.Began:
        //             if(isFirstClicked){
        //                 if(!isLocked && hit.collider != null){
        //                     SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    
        //                     Debug.Log(++GameManager.Instance.totalOfCandies);
        //                     isLocked = true;
        //                     sp.color = curColor;
        //                 }
        //             }
        //         break;

        //     }
        // }
        curColor = colorList[colorCount];

        var ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit  = Physics2D.Raycast(ray, -Vector2.up);

        if(Input.GetButton("Fire1")) {
            if(isFirstClicked){
                if(hit.collider != null && !isLocked){
                    
                    SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                    
                    //Color currentColor = sp.color;
                    Color currentColor = new Color(1.000f, 1.000f, 1.000f, 1.000f);
                    if((int)(currentColor.r * 1000) == (int)(sp.color.r * 1000) &&
                        (int)(currentColor.g * 1000) == (int)(sp.color.g * 1000))
                    {
                        //Debug.Log("Happy!");
                        //progressBar.value++;
                    
                        Debug.Log(++progressBar.value);
                        scoreKeeper.IncrementCorrectAnswer();
                        scoreKeeper.IncrementQuesitonsSeen();
                    }
                    if(progressBar.value == progressBar.maxValue)
                    {
                        scoreKeeper.IncrementCorrectAnswer();
                        isComplete = true;
                        ShowCompletion();
                        //Debug.Log(progressBar.value+ "|"+ progressBar.maxValue);
                    }
                    
                    //isLocked = true;
                    sp.color = curColor;


                   
                }
            }
        }
    }

    void ShowCompletion()
    {
        EventCenter.GetInstance().EventTrigger("UpdateScorePercent");
    }

    public void Paint(int colorCode) {

        colorCount = colorCode;
        //isLocked = false;
        isFirstClicked = true;
        
    }
}
