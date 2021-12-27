using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LineTool : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler
{
    LineTracing lineTracing;

    private void Start() {
        
        lineTracing = FindObjectOfType<LineTracing>();
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        int myParseInt = int.Parse(gameObject.name);

        if(myParseInt == lineTracing.ctr)
        {
            
            // - lineTracing.lr.transform.position
            // lineTracing.lr
            //     .SetPosition(lineTracing.x, lineTracing
            //         .myRectTransformPos[lineTracing.x].transform.position);
                    

            //  Vector3 endPos = lineTracing.lr.GetPosition(lineTracing.lr.positionCount - 1); 
            lineTracing.audioSource.PlayOneShot(lineTracing.traceSound, 0.7f);
            lineTracing.ctr++;
            lineTracing.fillImage[lineTracing.nextImageTofillNum].fillAmount += lineTracing.amountToAdd[lineTracing.nextImageTofillNum];
            //Debug.Log(lineTracing.lenOfImg[lineTracing.nextImageTofillNum]);
            if(lineTracing.ctr == lineTracing.lenOfImg[lineTracing.nextImageTofillNum])
            {
                
                //lineTracing.fillImage[lineTracing.nextImageTofillNum].fillAmount += lineTracing.amountToAdd[lineTracing.nextImageTofillNum];
                
                lineTracing.fillImage[lineTracing.nextImageTofillNum].fillAmount += lineTracing.amountToAdd[lineTracing.nextImageTofillNum];
                lineTracing.nextImageTofillNum++;

                //Debug.Log(lineTracing.fillImage.Length);

            }

        }

        if(lineTracing.totalOfTracing == lineTracing.nextImageTofillNum)
        {
            lineTracing.finger.SetActive(false);
            Debug.Log("Goods ah");
        }
            
    }
}
