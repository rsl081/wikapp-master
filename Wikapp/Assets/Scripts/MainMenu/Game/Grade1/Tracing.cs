using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tracing : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Image traceImge;
    [SerializeField]float amountToAdd = 0.02f;
    [SerializeField] float howFar = 6f;
    private RectTransform draggingObject;
    private Vector3 velocity = Vector3.zero;
    // Vector2 starTouchPosition;
    // Vector2 endPosition;
    // Vector2 currentPosition;
    // bool stopTouch = false;
    bool isEndDragEnd = true;

    private void Start() {
        traceImge = GetComponent<Image>();
        traceImge.fillAmount = 0f;
        draggingObject = GetComponent<RectTransform>();

    }


    public void OnDrag(PointerEventData eventData)
    {
        //! The best here is swipe
        Debug.Log(traceImge.color);
   
        if(traceImge.fillAmount != 1){
            isEndDragEnd = true;
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObject, 
                eventData.position, eventData.pressEventCamera, out var globalMousePosition))
            {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


                    if(globalMousePosition.x == touchPos.x && globalMousePosition.y == touchPos.y
                    && Vector2.Distance(draggingObject.transform.position, touchPos) < howFar){
                        traceImge.fillAmount += amountToAdd;
                    }
            }
        }
        //Debug.Log("call");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin");
        //sEndDragEnd = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
   
        if((traceImge.fillAmount == 1 && isEndDragEnd) || (traceImge.fillAmount >= 0.89 && isEndDragEnd))
        {
            traceImge.raycastTarget = false;
            FindObjectOfType<QuizNumberTracing>().numberOfCorrectTrace++;

            if(FindObjectOfType<QuizNumberTracing>().numberOfCorrectTrace ==
                FindObjectOfType<QuizNumberTracing>().lenOfTracing-1){

                FindObjectOfType<QuizNumberTracing>().OnAnswerSelected();
       
                isEndDragEnd = false;
            }
        } 
    }


}
