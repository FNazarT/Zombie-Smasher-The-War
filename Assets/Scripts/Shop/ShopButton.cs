using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Text itemNameText = default;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.interactable = false;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    public void ActivateButton()
    {
        button.onClick.AddListener(HandleClick);
        button.interactable = true;
    }

    public void HandleClick()
    {
        AdManager.instance.ShowAdShop(itemNameText.text);
    }
}
