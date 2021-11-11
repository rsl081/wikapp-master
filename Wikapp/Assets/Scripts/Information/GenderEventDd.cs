using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GenderEventDd : EventTrigger
{
    bool isClick;
    public override void OnPointerDown(PointerEventData eventData)
    {
      if(!isClick){
        FindObjectOfType<InfoGUI>().genderDropDown.options.RemoveAt(0);
        FindObjectOfType<InfoGUI>().genderDropDown.captionText.fontStyle = FontStyles.Normal;     
        FindObjectOfType<InfoGUI>().genderDropDown.captionText.color = Color.white;     
        FindObjectOfType<InfoGUI>().genderDropDown.RefreshShownValue();
        isClick = true;
      }
    }

}
