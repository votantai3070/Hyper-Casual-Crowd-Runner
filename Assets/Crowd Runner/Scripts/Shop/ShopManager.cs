using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("Elements")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private PlayerSelector playerSelector;
    private System.Action<int> onSkinSelected;

    [Header("Skin")]
    [SerializeField] private Sprite[] skins;
    [SerializeField] GameObject objectPool;

    [Header("Price")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        priceText.text = skinPrice.ToString();

    }

    private void Start()
    {
        SelectSkin(GetLastSelectedSkin());

        ConfigureButtons();

        onSkinSelected += GetSkinSelectedIndex;
    }

    private void OnDestroy()
    {
        onSkinSelected -= GetSkinSelectedIndex;
    }

    private void GetSkinSelectedIndex(int skinIndex)
    {
        playerSelector.SelectSkin(skinIndex);
    }

    private void Update()
    {
        UpdatePurchaseButton();
    }

    private void ConfigureButtons()
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;

            skinButtons[i].Configure(skins[i], unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex)
    {
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlocked();
    }


    private void SelectSkin(int skinIndex)
    {
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (skinIndex == i)
                skinButtons[i].Select();

            else
                skinButtons[i].Deselect();
        }
        onSkinSelected?.Invoke(skinIndex);

        SaveLastSelectedSkin(skinIndex);

        for (int i = 0; i < objectPool.transform.childCount; i++)
        {
            if (objectPool.transform.GetChild(i).CompareTag("Runner"))
            {
                objectPool.transform.GetChild(i).GetComponent<RunnerSelector>().SelectRunner(skinIndex);
            }
        }
    }

    public void PurchaseSkinBtn()
    {
        List<SkinButton> skinButtonsList = new();

        for (int i = 0; i < skinButtons.Length; i++)
        {
            if (!skinButtons[i].IsUnlocked())
                skinButtonsList.Add(skinButtons[i]);
        }

        if (skinButtonsList.Count <= 0) return;

        SkinButton randomSkinButton = skinButtonsList[Random.Range(0, skinButtonsList.Count)];

        UnlockSkin(randomSkinButton.transform.GetSiblingIndex());
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataCoinManager.instance.UseCoins(skinPrice);

    }

    public void UpdatePurchaseButton()
    {
        if (DataCoinManager.instance.GetCoin() < skinPrice)
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;
    }

    private int GetLastSelectedSkin() => PlayerPrefs.GetInt("lastSelectedSkin", 0);

    private void SaveLastSelectedSkin(int skinIndex) => PlayerPrefs.SetInt("lastSelectedSkin", skinIndex);
}
