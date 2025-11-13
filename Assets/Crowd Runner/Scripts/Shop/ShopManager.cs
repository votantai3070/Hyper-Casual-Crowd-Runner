using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Button purchaseButton;

    [Header("Skin")]
    [SerializeField] private Sprite[] skins;

    [Header("Price")]
    [SerializeField] private int skinPrice;
    [SerializeField] private TextMeshProUGUI priceText;

    private void Awake()
    {
        priceText.text = skinPrice.ToString();
    }

    private void Start()
    {
        ConfigureButtons();
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

    private void UnlockSkin(SkinButton skinButton)
    {
        int skinIndex = skinButton.transform.GetSiblingIndex();

        UnlockSkin(skinIndex);
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

        UnlockSkin(randomSkinButton);
        SelectSkin(randomSkinButton.transform.GetSiblingIndex());

        DataCoinManager.instance.UseCoins(skinPrice);

    }

    private void UpdatePurchaseButton()
    {
        if (DataCoinManager.instance.GetCoin() < skinPrice)
            purchaseButton.interactable = false;
        else
            purchaseButton.interactable = true;
    }
}
