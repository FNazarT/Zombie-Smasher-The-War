using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public static InventoryPanel instance;

    [SerializeField] private Button lifeButton;
    [SerializeField] private Button shootEnergyButton;
    [SerializeField] private Button forceFieldButton;
    [SerializeField] private Text lifeAmountText;
    [SerializeField] private Text shootAmountText;
    [SerializeField] private Text fieldAmountText;

    private int lifeAmount, shootAmount, fieldAmount;

    void Awake()
    {
        instance = this;

        lifeAmount = PlayerPrefs.GetInt("Life");
        lifeAmountText.text = "x " + lifeAmount;

        shootAmount = PlayerPrefs.GetInt("Shoot Energy");
        shootAmountText.text = "x " + shootAmount;

        fieldAmount = PlayerPrefs.GetInt("Force Field");
        fieldAmountText.text = "x " + fieldAmount;
    }

    private void Update()
    {
        lifeButton.interactable = lifeAmount != 2;
        shootEnergyButton.interactable = shootAmount != 2;
        forceFieldButton.interactable = fieldAmount != 2;
    }

    public void SetInventoryValue(string purchasedItem)
    {
        switch (purchasedItem)
        {
            case "Life":
                lifeAmount++;
                PlayerPrefs.SetInt("Life", lifeAmount);
                lifeAmountText.text = "x " + lifeAmount;
                break;

            case "Shoot Energy":
                shootAmount++;
                PlayerPrefs.SetInt("Shoot Energy", shootAmount);
                shootAmountText.text = "x " + shootAmount;
                break;

            case "Force Field":
                fieldAmount++;
                PlayerPrefs.SetInt("Force Field", fieldAmount);
                fieldAmountText.text = "x " + fieldAmount;
                break;
        }
    }
}
