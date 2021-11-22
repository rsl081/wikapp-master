using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlColoring : MonoBehaviour
{
    public SpriteRenderer[] whiteColor;
    // Start is called before the first frame update
    void Start()
    {
        whiteColor = GetComponentsInChildren<SpriteRenderer>();
    }


}
