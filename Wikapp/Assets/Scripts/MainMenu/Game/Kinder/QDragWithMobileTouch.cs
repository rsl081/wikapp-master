using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class QDragWithMobileTouch : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private bool startDragging = true;
    QDragWithText quizWithText;
    [SerializeField] float distanceOfSnap = 1.5f;
    [SerializeField] private float dampingSpeed = 0.05f; //The closer to zero the faster it goes
    private RectTransform draggingObject;
    private Vector3 velocity = Vector3.zero;
    int answer;
    private void Awake() {
        quizWithText = FindObjectOfType<QDragWithText>();
        draggingObject = GetComponent<RectTransform>();
        answer = int.Parse(this.gameObject.name);
    }
    public void OnDrag(PointerEventData data)
    {
        if(startDragging){
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObject, 
                data.position, data.pressEventCamera, out var globalMousePosition))
            {
                draggingObject.position = Vector3.SmoothDamp(draggingObject.position, 
                    globalMousePosition, ref velocity, dampingSpeed);
            }
        }
    }
   
    public void OnEndDrag(PointerEventData eventData)
    {
        
        if(startDragging){
            
            quizWithText.PauseSound(answer);

            for(int i = 0; i < quizWithText.answerText.Length; i++){
                Image btnText = quizWithText.answerText[i].GetComponentInChildren<Image>();
                if(Vector2.Distance(btnText.gameObject.transform.position, quizWithText.questionImage.transform.position) < distanceOfSnap){
                    
                    btnText.gameObject.transform.position = quizWithText.questionImage.transform.position;
                    startDragging = false;
                    Invoke(nameof(GoForAnswer), .5f);

                }

            }
        }
    }

    void GoForAnswer()
    {
        quizWithText.OnAnswerSelected(answer);
        startDragging = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        quizWithText.PlaySound(answer);
    }
}
