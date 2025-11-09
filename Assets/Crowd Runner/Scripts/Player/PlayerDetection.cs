using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    private CrowdSystem crowdSystem;

    void Start()
    {
        crowdSystem = GetComponent<CrowdSystem>();
    }

    void Update()
    {
        Detected();
    }

    private void Detected()
    {
        Collider[] detectedCollider = Physics.OverlapSphere(transform.position, 1);

        foreach (var collider in detectedCollider)
        {
            if (collider.TryGetComponent<Doors>(out var doors))
            {
                Debug.Log("We hit some doors");

                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                doors.Disable();

                crowdSystem.ApplyBonus(bonusAmount, bonusType);
            }

            else if (collider.CompareTag("Finish"))
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);

                SceneManager.LoadScene(0);
            }
        }
    }
}
