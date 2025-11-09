using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Slider progressBarSlider;

    private void Start()
    {
        gamePanel.SetActive(false);

        progressBarSlider.value = 0;

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);

    }

    private void Update()
    {
        UpdateProgressBar();
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
