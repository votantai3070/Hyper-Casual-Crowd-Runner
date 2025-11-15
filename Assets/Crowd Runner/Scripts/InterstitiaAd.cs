using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitiaAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static InterstitiaAd instance;

    [SerializeField] string androidAdUnitId;
    [SerializeField] string iOSAdUnitId;

    string adUnitId;
    private bool adLoaded;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? iOSAdUnitId : androidAdUnitId;
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + adUnitId);
        Advertisement.Load(adUnitId, this);
    }

    public void ShowAd()
    {
        if (!adLoaded)
        {
            Debug.LogWarning("Ad not loaded yet!");
            return;
        }

        Debug.Log("Showing Ad: " + adUnitId);
        Advertisement.Show(adUnitId, this);
        adLoaded = false;
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("Ad Loaded: " + placementId);
        adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Ad Failed to Load: {placementId} - Error: {error} - {message}");
        adLoaded = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad Show Failed: {placementId} - Error: {error} - {message}");
        LoadAd();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("Ad Show Started: " + placementId);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("Ad Clicked: " + placementId);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad Show Complete: " + placementId + " - State: " + showCompletionState);
        LoadAd(); // Load ad mới sau khi xem xong
    }
}
