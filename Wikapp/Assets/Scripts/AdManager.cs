using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool testMode = false;
    public static AdManager Instance;

    private GameManager gameManager;

    #if UNITY_ANDROID
    private string gameId = "4552781";
    #endif

    private void Awake() {

        // if(Instance != null && Instance != this)
        // {
        //     Destroy(this);
        // }else
        // {
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);

        // }
      
    }

    private void Start() {

        gameManager = FindObjectOfType<GameManager>();
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowAd(GameManager gameManager)
    {
        Debug.LogWarning("Ayaw?");
        this.gameManager = gameManager;
        Advertisement.Show("Rewarded_Android");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Unity ads error: {message}");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch(showResult){
            case ShowResult.Finished:

                gameManager.OnReplayLevel();

            break;
            case ShowResult.Skipped:

            break;
            case ShowResult.Failed:
                Debug.LogWarning("Ad Failed");
            break;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"Ad Started");
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"Unity ads ready");
    }


}