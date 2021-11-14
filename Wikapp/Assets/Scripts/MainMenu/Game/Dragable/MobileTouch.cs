using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileTouch : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private float dampingSpeed = 0.05f; //The closer to zero the faster it goes

    private RectTransform draggingObject;
    public RectTransform snapingPoint;
    private Vector3 velocity = Vector3.zero;

    private void Awake() {
        draggingObject = GetComponent<RectTransform>();
    }
 
    public void OnDrag(PointerEventData data)
    {
        // if(Vector2.Distance(draggingObject.position,snapingPoint.position) > 4){
        // }

        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(draggingObject, 
            data.position, data.pressEventCamera, out var globalMousePosition))
        {
            //Vector2 x = RectTransformUtility.ScreenPointToWorldPointInRectangle(snapingPoint);
            draggingObject.position = snapingPoint.position;

            draggingObject.position = Vector3.SmoothDamp(draggingObject.position, 
                globalMousePosition, ref velocity, dampingSpeed);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
