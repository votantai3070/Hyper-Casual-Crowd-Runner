using System;
using UnityEngine;

public enum SoundEffect { DoorHit, RunnerDie, GameOver, CompletedLevel, }

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Sound Event")]
    public static Action<SoundEffect> onChangeSoundEffect;

    [SerializeField] private AudioSource doorHitSource;
    [SerializeField] private AudioSource runnerDieSource;
    [SerializeField] private AudioSource levelCompleteSource;
    [SerializeField] private AudioSource gameOverSource;
    [SerializeField] private AudioSource buttonSource;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
    }

    private void Start()
    {
        onChangeSoundEffect += GameSoundStateChangedCallback;
    }

    private void OnDestroy()
    {
        onChangeSoundEffect -= GameSoundStateChangedCallback;
    }

    private void GameSoundStateChangedCallback(SoundEffect sound)
    {
        switch (sound)
        {
            case SoundEffect.DoorHit:
                doorHitSource.Play();
                break;

            case SoundEffect.RunnerDie:
                runnerDieSource.Play();
                break;

            case SoundEffect.GameOver:
                gameOverSource.Play();
                break;

            case SoundEffect.CompletedLevel:
                levelCompleteSource.Play();
                break;
        }
    }

    public void SetSoundEffect(SoundEffect sound)
    {
        onChangeSoundEffect?.Invoke(sound);
    }

    internal void DisableSounds()
    {
        doorHitSource.volume = 0;
        runnerDieSource.volume = 0;
        levelCompleteSource.volume = 0;
        gameOverSource.volume = 0;
        buttonSource.volume = 0;
    }

    internal void EnableSounds()
    {
        doorHitSource.volume = 0.02f;
        runnerDieSource.volume = 0.02f;
        levelCompleteSource.volume = 0.02f;
        gameOverSource.volume = 0.02f;
        buttonSource.volume = 0.02f;
    }
}
