using System;
using UnityEngine;

public class CrowSystem : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform runnersParent;
    [SerializeField] GameObject runnerPrefab;

    [Header("Setting")]
    [SerializeField] float radius;
    [SerializeField] float angle;

    void Update()
    {
        PlaceRunners();
    }

    private void PlaceRunners()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Vector3 childLocalPosition = GetRunnerLocalPosition(i);
            runnersParent.GetChild(i).localPosition = childLocalPosition;
        }
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        //Debug.Log($"Cos {index}:" + Mathf.Cos(Mathf.Deg2Rad * index * angle));
        //Debug.Log($"Sin {index}:" + Mathf.Sin(Mathf.Deg2Rad * index * angle));

        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle); //x = R * sqrt(i) * cos(θi​)
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle); //z = R * sqrt(i) * sin(θi​)

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius() => radius * Mathf.Sqrt(runnersParent.childCount);

    public void ApplyBonus(int bonusAmount, BonusType bonusType)
    {
        switch (bonusType)
        {
            case BonusType.Addition:
                AddRunner(bonusAmount);
                break;

            case BonusType.Product:
                int runnerToAdd = (runnersParent.childCount * bonusAmount) - runnersParent.childCount;
                AddRunner(runnerToAdd);
                break;

            case BonusType.Diffirence:
                RemoveRunner(bonusAmount);
                break;

            case BonusType.Division:
                int runnerToDivide = runnersParent.childCount - (runnersParent.childCount / bonusAmount);
                RemoveRunner(runnerToDivide);
                break;
        }
    }

    private void AddRunner(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(runnerPrefab, runnersParent);
        }
    }

    private void RemoveRunner(int amount)
    {
        if (amount > runnersParent.childCount)
            amount = runnersParent.childCount;

        int runnerAmount = runnersParent.childCount;

        for (int i = runnerAmount - 1; i >= runnerAmount - amount; i--)
        {
            Transform runnerToDestroy = runnersParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
