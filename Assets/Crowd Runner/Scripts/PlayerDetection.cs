using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    private CrowSystem crowdSystem;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        crowdSystem = GetComponent<CrowSystem>();
    }

    // Update is called once per frame
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
                SceneManager.LoadScene(0);
            }
        }
    }
}
