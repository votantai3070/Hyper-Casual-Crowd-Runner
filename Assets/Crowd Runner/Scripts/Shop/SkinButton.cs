using System;
using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Button button;
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selector;

    private bool unlocked = false;

    public void Configure(Sprite skinSprite, bool unlocked)
    {
        skinImage.sprite = skinSprite;
        this.unlocked = unlocked;

        if (unlocked)
            Unlocked();
        else
            Lock();
    }

    public void Unlocked()
    {
        button.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);

        unlocked = true;
    }

    private void Lock()
    {
        button.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
    }

    public void Select()
    {
        selector.SetActive(true);
    }

    public void Deselect()
    {
        selector.SetActive(false);
    }

    public bool IsUnlocked() => unlocked;

    public Button GetButton() => button;
}
