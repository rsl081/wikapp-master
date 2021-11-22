using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoyColoring : MonoBehaviour
{
    public SpriteRenderer[] whiteColor;
    // Start is called before the first frame update
    void Start()
    {
        whiteColor = GetComponentsInChildren<SpriteRenderer>();
    }



  
}
