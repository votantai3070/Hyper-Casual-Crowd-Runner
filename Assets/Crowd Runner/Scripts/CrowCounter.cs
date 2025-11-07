using TMPro;
using UnityEngine;

public class CrowCounter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshPro crowdCounterText;
    [SerializeField] private Transform runnersParent;

    private void Update()
    {
        crowdCounterText.text = runnersParent.childCount.ToString();
    }
}
