using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName = "QuesitonTracingNumber", fileName ="Tracing Question 1")]
public class TracingNumberSO : ScriptableObject
{
    [SerializeField] GameObject question;

    public GameObject GetQuestion()
    {
        return question;
    }

}
