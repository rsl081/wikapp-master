using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FluidUI : MonoBehaviour
{
    public RectTransform mainPanel, setting, kinder, grade1, backPanelHome, howToPlay;
    //public RectTransform mainPanel, setting, kinder, grade1, grade2, grade3;

    public void AnimateUIBtn(string value)
    {
        switch (value)
        {
            case "MainMenu":
                MoveUI(setting, new Vector2(0, 1954), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;
            case "BackToMain":
                MoveUI(backPanelHome, new Vector2(-1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;
            
            case "Kinder":
                MoveUI(mainPanel, new Vector2(-1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(kinder, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                break;

            case "Grade1":
                MoveUI(mainPanel, new Vector2(-1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(grade1, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                break;

            case "HowToPlay":
                MoveUI(mainPanel, new Vector2(-1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(howToPlay, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                FindObjectOfType<VoiceManager>().PlayVoice();
                break;

            case "Settings":
                MoveUI(mainPanel, new Vector2(-1108, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(setting, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;
            case "KinderHomeBtn":
                MoveUI(kinder, new Vector2(1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                break;
            case "Grade1HomeBtn":
                MoveUI(grade1, new Vector2(1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                break;
            case "HowToPlayToHome":
                MoveUI(howToPlay, new Vector2(1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(mainPanel, new Vector2(0, 0), 0.25f, 0f, Ease.OutFlash);
                FindObjectOfType<VoiceManager>().StopVoice();
                break;





            //(Kinder)Quarter To Game then Back To HOME
            case "KndrGameToHome":
                MoveUI(kinder, new Vector2(1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(null, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;

            //(Grade 1)Quarter To Game then Back To HOME
            case "G1GameToHome":
                MoveUI(grade1, new Vector2(1128, 0), 0.25f, 0f, Ease.OutFlash);
                MoveUI(null, new Vector2(0, 0), 0.25f, 0.25f, Ease.OutFlash);
                break;

        
        }
    }

    void FadeEffect(Image _image, float fadeTo, float fadeDuration, float delay)
    {
        _image.DOFade(fadeTo, fadeDuration);
        _image.DOFade(1, fadeDuration).SetDelay(delay).OnComplete(() => FadeEffect(_image, fadeTo, fadeDuration, delay));
    }

    void MoveUI(RectTransform _traansform, Vector2 position, float moveTime, float delayTime, Ease ease)
    {
        _traansform.DOAnchorPos(position, moveTime).SetDelay(delayTime).SetEase(ease);
    }
}
