using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Text itemNameText = default;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HandleClick);
    }

    void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void HandleClick()
    {
        AdManager.instance.RewardedAdShop(itemNameText.text);
    }
}
