using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private bool tryAgain = true;
    private int highestScore, newScore, remainingEnergy, fieldAmount, lifeAmount, shootAmount, zombieKillCount = 0, healthValue = 100;
    private readonly float fillTime = 0.01f;
    private readonly int increaseEnergy = 1;
    private readonly int energyPortion = 10;
    private readonly int fullEnergy = 100;

    [SerializeField] private AudioSource musicLevel;
    [SerializeField] private Button forceFieldButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject continueWithAdPanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject howToPlayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider shootBar;
    [SerializeField] private Text fieldAmountText;
    [SerializeField] private Text lifeAmountText;
    [SerializeField] private Text shootAmountText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text bestScoreText;

    void Awake() => instance = this;

    void Start()
    {
        highestScore = PlayerPrefs.GetInt("Highest Score");

        fieldAmount = PlayerPrefs.GetInt("Force Field");
        fieldAmountText.text = "x " + fieldAmount;
        forceFieldButton.interactable = fieldAmount > 0;

        lifeAmount = PlayerPrefs.GetInt("Life");
        lifeAmountText.text = "x " + lifeAmount;

        shootAmount = PlayerPrefs.GetInt("Shoot Energy");
        shootAmountText.text = "x " + shootAmount;

        healthBar.value = healthValue;

        remainingEnergy = fullEnergy;
        shootBar.value = remainingEnergy;
    }

    //Called from ExplosiveObstacle Script when the players collides with an obstacle
    public void ApplyDamage(int damageAmount)
    {
        healthValue -= damageAmount;
        if (healthValue < 0) { healthValue = 0; }
        healthBar.value = healthValue;

        if (healthBar.value == 0)
        {
            //Evaluate if player has extra lives
            if (lifeAmount > 0)
            {
                healthValue = 100;
                healthBar.value = healthValue;
                lifeAmount--;
                lifeAmountText.text = "x " + lifeAmount;
                PlayerPrefs.SetInt("Life", lifeAmount);
            }
            else
            {
                //Player ran out of life and the game evaluates and offers a chance to continue after watching an ad
                TryAgain();
            }
        }
    }

    //Called from PlayerController Script when players shoots a bullet
    public void ShootEnergy()
    {
        remainingEnergy -= energyPortion;
        shootBar.value = remainingEnergy;

        if (shootBar.value == 0)
        {
            player.GetComponent<PlayerController>().canShoot = false;
            if (shootAmount > 0)
            {
                shootAmount--;
                shootAmountText.text = "x " + shootAmount;
                PlayerPrefs.SetInt("Shoot Energy", shootAmount);
                StartCoroutine(nameof(FillEnergy));
            }
        }
    }

    //Called from ForceField Script when player activates a force field
    public void HandleForceFieldCount()
    {
        fieldAmount--;
        fieldAmountText.text = "x " + fieldAmount;
        PlayerPrefs.SetInt("Force Field", fieldAmount);
        forceFieldButton.interactable = fieldAmount > 0;
    }

    //Called from ZombieScript script when player smashes a zombie
    public void IncreaseScore()
    {
        zombieKillCount++;
        scoreText.text = zombieKillCount.ToString();

        //Restore 25% of health every time player kills 100 zombies
        if (zombieKillCount % 100 == 0)
        {
            healthValue += 25;
            if (healthValue > 100) { healthValue = 100; }
            healthBar.value = healthValue;
        }

        //Restore all shoot energy every time player kills 50 zombies
        if (zombieKillCount % 50 == 0)
        {
            StartCoroutine(nameof(FillEnergy));
        }
    }

    //Refill shoot energy bar
    IEnumerator FillEnergy()
    {
        while (remainingEnergy < fullEnergy)
        {
            remainingEnergy += increaseEnergy;
            shootBar.value = remainingEnergy;

            yield return new WaitForSeconds(fillTime);
        }
        player.GetComponent<PlayerController>().canShoot = true;
    }

    //Called when players runs out of lives
    public void TryAgain()
    {
        UIPanel.SetActive(false);
        player.GetComponent<AudioSource>().Stop();      //Stops the sound of the tank
        musicLevel.Pause();                             //Pauses the level music
        Time.timeScale = 0f;

        //Evaluate if player can continue. If accepted, no more chances to continue
        if (tryAgain && Application.platform != RuntimePlatform.WebGLPlayer)
        {
            continueWithAdPanel.SetActive(true);
            tryAgain = false;
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameoverPanel.SetActive(true);
        finalScoreText.text = "Mataste: " + zombieKillCount;

        //This section evaluates if there's a new Best Score, then shows it and stores it
        newScore = zombieKillCount;
        if (newScore > highestScore)
        {
            PlayerPrefs.SetInt("Highest Score", newScore);
            bestScoreText.text = newScore.ToString();
        }
        else if (newScore <= highestScore)
        {
            bestScoreText.text = highestScore.ToString();
        }
    }

    //Called from AdManager Script after the Unity Ad is finished
    public void ContinueAfterAd()
    {
        UIPanel.SetActive(true);
        healthValue += 50;
        healthBar.value = healthValue;
        ResumeGame();
    }

    //Called from ContinueAfterAd Method and from "Resume" Button in the Pause Panel
    public void ResumeGame()
    {
        player.GetComponent<AudioSource>().Play();      //Resumes the sound of the tank
        musicLevel.Play();                              //Resumes the music level
        Time.timeScale = 1f;
        pausePanel.SetActive(false);                    //Only necessary when ResumeGame() is called from the Resume Button in the Pause Panel
    }

    //------ These Methods are called from UI Buttons in Gameplay Scene ------

    public void PauseGame()
    {
        player.GetComponent<AudioSource>().Stop();      //Stops the sound of the tank
        musicLevel.Pause();                             //Pauses the music level
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ExitGamePlay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void DidContinueWithAd()
    {
        continueWithAdPanel.SetActive(false);
        AdManager.instance.ShowAdGameplay();
    }

    public void DidNotContinueWithAd()
    {
        continueWithAdPanel.SetActive(false);
        GameOver();
    }

    public void OpenHowToPlayPanel() => howToPlayPanel.SetActive(true);

    public void CloseHowToPlayPanel() => howToPlayPanel.SetActive(false);
}
