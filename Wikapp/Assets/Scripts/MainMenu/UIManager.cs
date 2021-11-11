using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Info.Instance.currentPlayer = Info.Instance.getPlayer();
        Player player = Info.Instance.currentPlayer;
        Debug.Log(player._name);
        Debug.Log(player._age);
        Debug.Log(player._month);
        Debug.Log(player._day);
        Debug.Log(player._year);
        Debug.Log(player._gender);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
