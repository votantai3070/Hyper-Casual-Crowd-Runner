using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ShopManager shopManager;

    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider progressBarSlider;

    private void Start()
    {
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);

        progressBarSlider.value = 0;

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);

        GameManager.onGameStateChanged += GameStateChangedCallback; ;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback; ;
    }


    private void Update()
    {
        UpdateProgressBar();
    }

    public void ShowShopPanel()
    {
        shopPanel.SetActive(true);
        menuPanel.SetActive(false);
        shopManager.UpdatePurchaseButton();
    }
    public void HideShopPanel()
    {
        shopPanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void HideSettingPanel()
    {
        settingsPanel.SetActive(false);
    }

    private void GameStateChangedCallback(GameState state)
    {
        if (state == GameState.GameOver)
            ShowGameOver();
        if (state == GameState.LevelComplete)
            ShowNext();
    }

    private void ShowNext()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void PressedRetryButton()
    {
        InterstitiaAd.instance.ShowAd();

        SceneManager.LoadScene(0);
    }

    private void ShowGameOver()
    {
        SoundManager.instance.SetSoundEffect(SoundEffect.GameOver);

        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void PressedPlayButton()
    {
        GameManager.instance.SetGameState(GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
            return;

        float process = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();

        progressBarSlider.value = process;
    }
}
