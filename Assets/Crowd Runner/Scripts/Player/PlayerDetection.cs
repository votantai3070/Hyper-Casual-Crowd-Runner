using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [Header("Element")]
    private CrowdSystem crowdSystem;
    private Collider[] detectedCollider;


    void Start()
    {
        detectedCollider = new Collider[5];

        crowdSystem = GetComponent<CrowdSystem>();
    }

    void Update()
    {
        if (GameManager.instance.IsGameState())
            Detected();
    }

    private void Detected()
    {

        int count = Physics.OverlapSphereNonAlloc(transform.position, 1, detectedCollider);

        for (int i = 0; i < count; i++)
        {
            if (detectedCollider[i].TryGetComponent<Doors>(out var doors))
            {
                Debug.Log("We hit some doors");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();

                SoundManager.instance.SetSoundEffect(SoundEffect.DoorHit);

                crowdSystem.ApplyBonus(bonusAmount, bonusType);
            }

            else if (detectedCollider[i].CompareTag("Finish"))
            {
                Debug.Log("Current level before detected line: " + PlayerPrefs.GetInt("level"));

                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                Debug.Log("Current level after detected line: " + PlayerPrefs.GetInt("level"));

                GameManager.instance.SetGameState(GameState.LevelComplete);
                SoundManager.instance.SetSoundEffect(SoundEffect.CompletedLevel);

                //SceneManager.LoadScene(0);
            }
        }
    }
}
