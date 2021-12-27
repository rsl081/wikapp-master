using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineTracing : MonoBehaviour
{
    Vector3 starPos;
    Vector3 endPos;
    public Camera mycamera;
    public Transform[] myRectTransformPos; 
    public int ctr = 0;
    public int nextImageTofillNum = 0;
    public int[] lenOfImg;

    public Image[] fillImage;

    public int totalOfTracing;

    public float[] amountToAdd;
    public GameObject finger;

    public AudioSource audioSource;
    public AudioClip traceSound;

    // Start is called before the first frame update
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        
        mycamera = Camera.main;

        fillImage[0].fillAmount = 0;

        //lr = GetComponent<LineRenderer>();
        Vector3 myTrans = Camera.main.ScreenToWorldPoint(myRectTransformPos[0].transform.position);

        this.transform.position = myRectTransformPos[0].transform.position;
    
            
      
        //lr.startWidth = 0.15f;

        // for(int i = 0; i < myRectTransformPos.Length; i++)
        // {

        //     Instantiate(linePrefab, myRectTransformPos[i].transform.position, transform.rotation);
            
         


        //     lineRenderer.SetPosition(i, myRectTransformPos[i].transform.position - lineRenderer.transform.position);


        // }

    }
}
