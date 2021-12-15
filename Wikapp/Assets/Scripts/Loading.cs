using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] Image loadingImage;
    [SerializeField] bool isBarAutoLoad = true;
    float timerValue = 0;
    Transition transition;

    // Start is called before the first frame update
    void Start()
    {
        
        transition = FindObjectOfType<Transition>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isBarAutoLoad){
            UpdateLoading();
        }
    }

    void UpdateLoading()
    {
        timerValue += Time.deltaTime;
        
        loadingImage.fillAmount = timerValue / transition.timeToWait;
    }

    
}
