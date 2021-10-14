//All methods in this script are called from their respective button

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject howToPlayPanel;

    public void PlayButton() => Camera.main.GetComponent<Animator>().Play("Slide");

    public void SettingsButton() => SceneManager.LoadScene("Settings");

    public void CreditsButton() => SceneManager.LoadScene("Credits");

    public void ShopButton() => SceneManager.LoadScene("Shop");

    public void BackToMenuButton() => SceneManager.LoadScene("Main Menu");

    public void ExitGameButton() => Application.Quit();

    public void OpenHowToPlayPanel() => howToPlayPanel.SetActive(true);

    public void CloseHowToPlayPanel() => howToPlayPanel.SetActive(false);
}