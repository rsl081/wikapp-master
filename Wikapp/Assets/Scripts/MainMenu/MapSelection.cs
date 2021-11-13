using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public bool isUnlock = false;
    public GameObject lockGo;
    public GameObject unlockGo;

    public int mapIndex;//the index of this map
    public int questNum;//How many candies can unlock this map
    public int startLevel;
    public int endLevel;

    private void Start()
    {
        EventCenter.GetInstance().AddEventListener("UpdateMap", UnlockMap);
        EventCenter.GetInstance().AddEventListener("UpdateMap", UpdateMapStatus);
    }
    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener("UpdateMap", UnlockMap);
        EventCenter.GetInstance().RemoveEventListener("UpdateMap", UpdateMapStatus);

    }

    private void UpdateMapStatus()
    {
        if(isUnlock)//We Can Play This MAP!
        {
            unlockGo.gameObject.SetActive(true);
            lockGo.gameObject.SetActive(false);
        }
        else//This Map still lock now. We have to complete his/her quest 
        {
            unlockGo.gameObject.SetActive(false);
            lockGo.gameObject.SetActive(true);
        }
    }

    private void UnlockMap()//If our current candies number is great than the request number which means we can unlock the next map 
    {
        if (UIManager.instance.candies >= questNum)
        {
            isUnlock = true;
        }
        else
        {
            isUnlock = false;
        }
    }


}
