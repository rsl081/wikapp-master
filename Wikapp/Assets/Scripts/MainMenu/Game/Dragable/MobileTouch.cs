using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileTouch : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private bool startDragging = true;
    QuizQuestionDrag quizQuestionDrag;
    [SerializeField] float distanceOfSnap = 1.5f;
    [SerializeField] private float dampingSpeed = 0.05f; //The closer to zero the faster it goes
    private RectTransform draggingObject;
    private Vector3 velocity = Vector3.zero;
    int answer;
    private void Awake() {
        quizQuestionDrag = FindObjectOfType<QuizQuestionDrag>();
        draggingObject = GetComponent<RectTransform>();
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
            answer = int.Parse(this.gameObject.name);
            for(int i = 0; i < quizQuestionDrag.answerImg.Length; i++){
                Image btnText = quizQuestionDrag.answerImg[i].GetComponentInChildren<Image>();
                if(Vector2.Distance(btnText.gameObject.transform.position, quizQuestionDrag.questionImage.transform.position) < distanceOfSnap){
                    
                    btnText.gameObject.transform.position = quizQuestionDrag.questionImage.transform.position;
                    startDragging = false;
                    Invoke(nameof(GoForAnswer), .5f);
                    //startDragging = false;
                }

            }
        }
    }

    void GoForAnswer()
    {
        quizQuestionDrag.OnAnswerSelected(answer);
        startDragging = true;
    }
    // private void Update() {

    //     if(startDragging){
    //         Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         transform.position = new Vector2(touchPos.x, touchPos.y);
    //     }

    // }

    // public override void OnPointerDown(PointerEventData data)
    // {
    //     startDragging = true;
    // }
    // public override void OnPointerUp(PointerEventData data)
    // {
    //     startDragging = false;
    //     if(this.gameObject.activeSelf){
    //         int answer = int.Parse(this.gameObject.name);
    //         for(int i = 0; i < quizQuestionDrag.answerImg.Length; i++){
    //             Image btnText = quizQuestionDrag.answerImg[i].GetComponentInChildren<Image>();
    //             if(Vector2.Distance(btnText.gameObject.transform.position, FindObjectOfType<QuizQuestionDrag>().questionImage.transform.position) < 3){
    //                 quizQuestionDrag.OnAnswerSelected(answer);
    //             }

    //         }
    //     }
    // }
}
