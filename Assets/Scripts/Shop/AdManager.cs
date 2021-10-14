using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    public static AdManager instance;

    private bool testMode = false;
    private string androidGameId = "4112687";
    private string androidAdUnitGameplay = "Android_Rewarded_Gameplay";
    private string androidAdUnitShop = "Android_Rewarded_Shop";
    private string purchasedItemName;

    void Awake() => instance = this;

    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(androidGameId, testMode);
    }

    //Remove listener event when the script is destroyed so that it doesn't cause perform issues when the scene is created again
    void OnDestroy() => Advertisement.RemoveListener(this);

    //Called from UIManager Script when player accepts to watch an ad to continue the game
    public void RewardedAdGamePlay() => Advertisement.Show(androidAdUnitGameplay);

    //Called from ShopButton script when player purchases an item 
    public void RewardedAdShop(string clickedItemName)
    {
        purchasedItemName = clickedItemName;
        Advertisement.Show(androidAdUnitShop);
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished) 
        {
            if (placementId == androidAdUnitShop) 
            {
                InventoryPanel.instance.SetInventoryValue(purchasedItemName); 
            } 
            else if(placementId == androidAdUnitGameplay) 
            {
                UIManager.instance.ContinueAfterAd();
            }            
        }
    }

    public void OnUnityAdsDidStart(string placementId) { }

    public void OnUnityAdsReady(string placementId) { }

    public void OnUnityAdsDidError(string message) { }
}
