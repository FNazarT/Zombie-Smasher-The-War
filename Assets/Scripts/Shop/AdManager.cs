using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdManager instance;

    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject messageText;
    private bool testMode = false;
    private string androidGameId = "4112687";
    private string androidAdUnitGameplay = "Android_Rewarded_Gameplay";
    private string androidAdUnitShop = "Android_Rewarded_Shop";
    private string adUnitId = null;
    private string purchasedItemName;

    void Awake() 
    {
        instance = this;

        if(Application.platform == RuntimePlatform.WebGLPlayer)
        {
            messageText.SetActive(true);
        }
        else
        {
            //Initialize Ads SDK
            Advertisement.Initialize(androidGameId, testMode, this);
            //Select AdUnit based on the scene we're currently in
            adUnitId = SceneManager.GetActiveScene().name == "Shop" ? androidAdUnitShop : androidAdUnitGameplay;
        }        
    }

    //Called from UIManager Script when player accepts to watch an ad to continue the game
    public void ShowAdGameplay() => Advertisement.Show(androidAdUnitGameplay, this);

    //Called from ShopButton script when player purchases an item 
    public void ShowAdShop(string clickedItemName)
    {
        purchasedItemName = clickedItemName;
        Advertisement.Show(androidAdUnitShop, this);
    }

    public void OnInitializationComplete()
    {
        // Load content to the Ad Unit
        Advertisement.Load(adUnitId, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Initialization Error: {error} - {message}");
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Unit successfully loaded");
        //Once the ad is successfully loaded, activate buttons
        if (adUnitId == androidAdUnitShop)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<ShopButton>().ActivateButton();
            }
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            if (adUnitId == androidAdUnitShop)
            {
                InventoryPanel.instance.SetInventoryValue(purchasedItemName);
            }
            else if (adUnitId == androidAdUnitGameplay)
            {
                UIManager.instance.ContinueAfterAd();
            }
        }
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message){ }

    public void OnUnityAdsShowStart(string adUnitId){ }

    public void OnUnityAdsShowClick(string adUnitId){ }
}
