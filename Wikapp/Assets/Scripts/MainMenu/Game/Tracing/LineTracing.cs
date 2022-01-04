using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LineTracing : MonoBehaviour
{
    Vector3 starPos;
    Vector3 endPos;
    public Camera mycamera;
    //public Transform[] myRectTransformPos; 
    public int ctr = 0;
    public int nextImageTofillNum = 0;
    public int[] lenOfImg;

    public Image[] fillImage;

    public int totalOfTracing;

    public float[] amountToAdd;
    public GameObject finger;
    public AudioSource audioSource;
    public GameObject correctVfx;
    public GameObject shakeGO;
    [SerializeField] AudioClip doneTracingSound;
    bool playOnce;
    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        
        mycamera = Camera.main;
        
        totalOfTracing = fillImage.Length;

        fillImage[0].fillAmount = 0;

        playOnce = true;
        
    }

    public void ShakeObject()
    {
        if(playOnce)
        {
            audioSource.PlayOneShot(doneTracingSound, 0.7f);
            shakeGO.transform.DOPunchPosition(shakeGO.transform.localPosition + 
                                                new Vector3(0f,-15f,0), 0.5f).Play();
            playOnce = false;
        }
        
    }
}
