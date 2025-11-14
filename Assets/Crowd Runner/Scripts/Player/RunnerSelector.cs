using UnityEngine;

public class RunnerSelector : MonoBehaviour
{
    [SerializeField] private Runner runner;

    public void SelectRunner(int runnerIndex)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (i == runnerIndex)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                runner.SetAnimator(transform.GetChild(i).GetComponent<Animator>());
            }
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
