using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class MthEventDd : EventTrigger
{
    bool isClick;
    public override void OnPointerDown(PointerEventData eventData)
    {
      if(!isClick){
        FindObjectOfType<InfoGUI>().monthDropDown.options.RemoveAt(0);
        FindObjectOfType<InfoGUI>().monthDropDown.captionText.fontStyle = FontStyles.Normal;     
        FindObjectOfType<InfoGUI>().monthDropDown.captionText.color = Color.white;     
        FindObjectOfType<InfoGUI>().monthDropDown.RefreshShownValue();
        isClick = true;
      }
    }
}
