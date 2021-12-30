using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        
        mycamera = Camera.main;
        
        totalOfTracing = fillImage.Length;

        fillImage[0].fillAmount = 0;


    }
}
