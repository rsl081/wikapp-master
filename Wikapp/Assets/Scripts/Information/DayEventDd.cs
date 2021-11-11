using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DayEventDd : EventTrigger
{

    bool isClick;
    public override void OnPointerDown(PointerEventData eventData)
    {
      if(!isClick){
        FindObjectOfType<InfoGUI>().dayDropDown.options.RemoveAt(0);
        FindObjectOfType<InfoGUI>().dayDropDown.captionText.fontStyle = FontStyles.Normal;     
        FindObjectOfType<InfoGUI>().dayDropDown.captionText.color = Color.white;     
        FindObjectOfType<InfoGUI>().dayDropDown.RefreshShownValue();
        isClick = true;
      }
    }
}
