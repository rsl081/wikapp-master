using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tracing : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public Image traceImge;
    float amountToAdd = 0.02f;
    [SerializeField] int howFar = 6;
    [SerializeField] private float dampingSpeed = 0.05f; //The closer to zero the faster it goes
    private RectTransform draggingObject;
    private Vector3 velocity = Vector3.zero;
    bool isEndDragEnd = true;

    private void Start() {
        traceImge = GetComponent<Image>();
        traceImge.fillAmount = 0f;
        draggingObject = GetComponent<RectTransform>();

    }


    public void OnDrag(PointerEventData eventData)
    {
        if(traceImge.fillAmount != 1){
            isEndDragEnd = true;
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObject, 
                eventData.position, eventData.pressEventCamera, out var globalMousePosition))
            {
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    if(globalMousePosition.x == touchPos.x
                    && Vector2.Distance(draggingObject.transform.position, touchPos) < howFar){
                        traceImge.fillAmount += amountToAdd;
                    }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //traceIamge.fillAmount += amountToAdd;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
   
        if(traceImge.fillAmount == 1 && isEndDragEnd)
        {
            FindObjectOfType<QuizNumberTracing>().numberOfCorrectTrace++;

            if(FindObjectOfType<QuizNumberTracing>().numberOfCorrectTrace ==
                FindObjectOfType<QuizNumberTracing>().lenOfTracing-1){

                FindObjectOfType<QuizNumberTracing>().OnAnswerSelected();
                
            }
            isEndDragEnd = false;
        }
        
        //traceIamge.fillAmount += amountToAdd;
    }
}
