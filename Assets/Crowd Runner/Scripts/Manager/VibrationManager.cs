using System;
using UnityEngine;

public class VibrationManager : MonoBehaviour
{
    public static VibrationManager instance;
    private bool haptics;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);

        instance = this;
    }

    private void Start()
    {
        SoundManager.onChangeSoundEffect += Vibration;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }


    private void OnDestroy()
    {
        SoundManager.onChangeSoundEffect -= Vibration;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    private void GameStateChangedCallback(GameState state)
    {
        if (state == GameState.LevelComplete || state == GameState.GameOver)
            Vibration(SoundEffect.DoorHit);
    }

    private void Vibration(SoundEffect effect)
    {
        if (effect == SoundEffect.DoorHit || effect == SoundEffect.RunnerDie)
            if (haptics)
                Taptic.Light();
    }

    internal void EnableVibrations() => haptics = true;

    internal void DisableVibrations() => haptics = false;
}
