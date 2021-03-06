using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class LineTool : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler
{
    LineTracing lineTracing;

    private void Start() {
        
        lineTracing = FindObjectOfType<LineTracing>();
       
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int myParseInt = int.Parse(gameObject.name);

        if(myParseInt == lineTracing.ctr)
        {

            //lineTracing.audioSource.PlayOneShot(lineTracing.traceSound, 0.7f);
            lineTracing.audioSource.PlayOneShot(lineTracing.audioSource.clip, 0.7f);
            Instantiate(lineTracing.correctVfx, transform.position, Quaternion.identity);
            lineTracing.ctr++;
            lineTracing.fillImage[lineTracing.nextImageTofillNum].fillAmount += lineTracing.amountToAdd[lineTracing.nextImageTofillNum];
        
            if(lineTracing.ctr == lineTracing.lenOfImg[lineTracing.nextImageTofillNum])
            {
                
                lineTracing.fillImage[lineTracing.nextImageTofillNum].fillAmount += lineTracing.amountToAdd[lineTracing.nextImageTofillNum];
                lineTracing.nextImageTofillNum++;
                FindObjectOfType<QuizNumberTracing>().numberOfCorrectTrace++;

            }

        }

        if(lineTracing.totalOfTracing == lineTracing.nextImageTofillNum)
        {
            lineTracing.finger.SetActive(false);
            lineTracing.ShakeObject();
           
            Invoke(nameof(DelayNextQuestion), 1f);

        }
            
    }

    void DelayNextQuestion()
    {
        FindObjectOfType<QuizNumberTracing>().OnAnswerSelected();

    }
}
