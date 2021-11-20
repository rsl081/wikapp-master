using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sound : EventTrigger
{
    [SerializeField] AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        audioSource.Play(0);
    }
    public override void OnPointerUp(PointerEventData data)
    {
        audioSource.Pause();
    }
}
