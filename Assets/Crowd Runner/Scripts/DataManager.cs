using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField] private TextMeshProUGUI[] coinsText;
    private int coins;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("coins", 0) > 0)
            PlayerPrefs.DeleteKey("coins");

        coins = PlayerPrefs.GetInt("coins", 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        foreach (var coin in coinsText)
        {
            coin.text = coins.ToString();
        }
    }

    public void AddCoins(int coinAmount)
    {
        coins += coinAmount;

        UpdateCoinsText();

        PlayerPrefs.SetInt("coins", coins);
    }
}
