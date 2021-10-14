using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItems
{
    public string itemName;
    public Sprite itemIcon;
}

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab = default;
    [SerializeField] private List<ShopItems> shopItems = default;

    void Start()
    {
        AddButtons();
    }

    public void AddButtons()
    {
        for(int i = 0; i < shopItems.Count; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(transform);
            //newButton.GetComponent<ShopButton>().SetupButton(shopItems[i]);
        }
    }
}
