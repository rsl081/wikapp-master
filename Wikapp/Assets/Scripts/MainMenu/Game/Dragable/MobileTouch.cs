using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MobileTouch : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private bool startDragging = true;
    QuizQuestionDrag quizQuestionDrag;
    QuizQuestionTextWithImage quizQuestionTextWithImage;
    [SerializeField] float distanceOfSnap = 1.5f;
    [SerializeField] private float dampingSpeed = 0.05f; //The closer to zero the faster it goes
    private RectTransform draggingObject;
    private Vector3 velocity = Vector3.zero;
    int answer;
    private void Awake() {
      
        quizQuestionDrag = FindObjectOfType<QuizQuestionDrag>();
        quizQuestionTextWithImage = FindObjectOfType<QuizQuestionTextWithImage>();

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

            if(quizQuestionDrag != null)
            {
                // quizQuestionDrag.questionImage.transform.DOPunchPosition(quizQuestionDrag.transform.localPosition + 
                //                                             new Vector3(0f,-5f,0), 0.5f).Play();
                answer = int.Parse(this.gameObject.name);
                for(int i = 0; i < quizQuestionDrag.answerImg.Length; i++){
                    Image btnText = quizQuestionDrag.answerImg[i].GetComponentInChildren<Image>();
                    if(Vector2.Distance(btnText.gameObject.transform.position, quizQuestionDrag.questionImage.transform.position) < distanceOfSnap){
                        
                        btnText.gameObject.transform.position = quizQuestionDrag.questionImage.transform.position;
                        startDragging = false;
                        GoForAnswerQuizQuestionDrag();
                    }

                }
            }else if(quizQuestionTextWithImage != null)
            {
                
                answer = int.Parse(this.gameObject.name);
                for(int i = 0; i < quizQuestionTextWithImage.answerImg.Length; i++){
                    Image btnText = quizQuestionTextWithImage.answerImg[i].GetComponentInChildren<Image>();
                    if(Vector2.Distance(btnText.gameObject.transform.position, quizQuestionTextWithImage.questionImage.transform.position) < distanceOfSnap){
                        
                        btnText.gameObject.transform.position = quizQuestionTextWithImage.questionImage.transform.position;
                        startDragging = false;
                        Invoke(nameof(GoForAnswerQuizQuestionTextWithImage), .5f);
                    }

                }
            }
        }
    }

    void GoForAnswerQuizQuestionDrag()
    {
        quizQuestionDrag.OnAnswerSelected(answer);
        startDragging = true;
    }
    void GoForAnswerQuizQuestionTextWithImage()
    {
        quizQuestionTextWithImage.OnAnswerSelected(answer);
        startDragging = true;
    }

}
