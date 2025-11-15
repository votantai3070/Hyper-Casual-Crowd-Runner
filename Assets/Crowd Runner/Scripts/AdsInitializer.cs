using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;
    private string _gameId;

    [Header("Element")]
    [SerializeField] private InterstitiaAd interstitiaAd;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        InitializeAds();
    }

    private void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSGameId : androidGameId;
        Advertisement.Initialize(_gameId, testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        if (interstitiaAd != null)
        {
            StartCoroutine(LoadAdWithDelay());
        }
    }

    IEnumerator LoadAdWithDelay()
    {
        yield return new WaitForSeconds(1f);
        interstitiaAd.LoadAd();
    }


    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads initialization failed: {error.ToString()} - {message}.");
    }
}
