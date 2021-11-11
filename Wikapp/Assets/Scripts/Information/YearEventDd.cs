using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class YearEventDd : EventTrigger
{
    bool isClick;
    public override void OnPointerDown(PointerEventData eventData)
    {
      if(!isClick){
        FindObjectOfType<InfoGUI>().yearDropDown.options.RemoveAt(0);
        FindObjectOfType<InfoGUI>().yearDropDown.captionText.fontStyle = FontStyles.Normal;     
        FindObjectOfType<InfoGUI>().yearDropDown.captionText.color = Color.white;     
        FindObjectOfType<InfoGUI>().yearDropDown.RefreshShownValue();
        isClick = true;
      }
    }
}
