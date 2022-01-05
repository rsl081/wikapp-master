using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PaintBucket : MonoBehaviour
{
    public Color[] colorList;
    public Color curColor;
    public int colorCount;
    bool isLocked = false;
    bool isFirstClicked = false;
    public bool isComplete;

    [SerializeField] Slider progressBar;

    ScoreKeeper scoreKeeper;

    [SerializeField] int numbersOfFillWhite;

    [SerializeField] Image[] borderImg;
    [SerializeField] GameObject clickingVfx;
    AudioSource source;
    [SerializeField] AudioClip btnSound;
    [SerializeField] AudioClip coloringSound;
    [SerializeField] AudioClip goodJobSound;
    GameObject[] spriteRenderers;

    private void Start() {

        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        source = GetComponent<AudioSource>();

        spriteRenderers = GameObject.FindGameObjectsWithTag("Coloring");

        progressBar.maxValue = numbersOfFillWhite;
        progressBar.value = 1;

        

    }

    private void OnDestroy() {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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
        RaycastHit2D hit  = Physics2D.Raycast(ray, -Vector2.up, 0.1f);

        // Debug.DrawRay(ray, -Vector2.up, Color.red);
        
        if(Input.GetButton("Fire1")) {
            if(isFirstClicked){
                if(hit.collider != null && !isLocked){
                    SpriteRenderer sp = hit.collider.gameObject.GetComponent<SpriteRenderer>();
                     
                    //Color currentColor = sp.color;
                    Color currentColor = new Color(1.000f, 1.000f, 1.000f, 1.000f);
                    if((int)(currentColor.r * 1000) == (int)(sp.color.r * 1000) &&
                        (int)(currentColor.g * 1000) == (int)(sp.color.g * 1000))
                    {
                        source.PlayOneShot(coloringSound, 0.7f);
                        Instantiate(clickingVfx, sp.transform.position, Quaternion.identity);
                    
                        Debug.Log(++progressBar.value);
                        scoreKeeper.IncrementCorrectAnswer();
                        scoreKeeper.IncrementQuesitonsSeen();
                    }
                    if((progressBar.value == progressBar.maxValue) && !isComplete)
                    {
                        source.PlayOneShot(goodJobSound, 0.7f);
                        for(int i = 0; i < spriteRenderers.Length; ++i){
                            
                            spriteRenderers[i].transform.DOPunchRotation(new Vector3(0f,0,10f), 0.5f).Play();

                        }
                    

                        scoreKeeper.IncrementCorrectAnswer();
                        isComplete = true;

                        ShowCompletion();
                    }
                    
                  
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

        

        for(int i = 0; i < borderImg.Length; i++)
        {
            if(i == colorCount)
            {
                source.PlayOneShot(btnSound, 0.7f);
                borderImg[i].color = new Color32(255,94,8,255);
                borderImg[i].transform.DOPunchPosition(transform.localPosition + 
                            new Vector3(0f, -10f,0f), 0.5f).Play();
            }else{

                borderImg[i].color = Color.white;
            
            }
        }
        
    }
}
