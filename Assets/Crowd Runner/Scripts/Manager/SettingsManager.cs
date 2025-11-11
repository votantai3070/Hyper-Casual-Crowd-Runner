using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Sprite optionOnSprite;
    [SerializeField] private Sprite optionOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    [Header("Settings")]
    private bool soundsState = true;
    private bool hapticsState = true;

    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("sound", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptic", 1) == 1;
    }

    private void Start()
    {
        InitialzeSetup();
    }

    private void InitialzeSetup()
    {
        if (soundsState)
            EnableSounds();
        else
            DisableSounds();

        if (hapticsState)
            EnableHaptics();
        else
            DisableHaptics();
    }


    public void ChangeSoundsState()
    {
        if (soundsState)
            DisableSounds();
        else
            EnableSounds();

        soundsState = !soundsState;

        PlayerPrefs.SetInt("sound", soundsState ? 1 : 0);
    }

    public void ChangeVibrationState()
    {
        if (hapticsState)
            DisableHaptics();
        else
            EnableHaptics();

        hapticsState = !hapticsState;

        PlayerPrefs.SetInt("haptic", hapticsState ? 1 : 0);
    }

    private void EnableHaptics()
    {
        VibrationManager.instance.EnableVibrations();

        hapticsButtonImage.sprite = optionOnSprite;
    }

    private void DisableHaptics()
    {
        VibrationManager.instance.DisableVibrations();

        hapticsButtonImage.sprite = optionOffSprite;
    }
    private void EnableSounds()
    {
        SoundManager.instance.EnableSounds();

        soundsButtonImage.sprite = optionOnSprite;
    }

    private void DisableSounds()
    {
        SoundManager.instance.DisableSounds();

        soundsButtonImage.sprite = optionOffSprite;
    }
}
