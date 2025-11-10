using System;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform enemyParent;

    [Header("Settings")]
    [SerializeField] private int enemyCount;
    [SerializeField] private float angle;
    [SerializeField] private float radius;

    private void Start()
    {
        IntializeEnemyGroup();
    }



    private void IntializeEnemyGroup()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            CreateNewEnemy(i);
        }
    }

    private void CreateNewEnemy(int i)
    {
        Enemy enemy = Instantiate(enemyPrefab, enemyParent);

        Vector3 worldTransform = transform.TransformPoint(GetRunnerLocalPosition(i));

        enemy.transform.position = worldTransform;

        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        enemy.transform.rotation = rotation;
    }

    private Vector3 GetRunnerLocalPosition(int index)
    {
        //Debug.Log($"Cos {index}:" + Mathf.Cos(Mathf.Deg2Rad * index * angle));
        //Debug.Log($"Sin {index}:" + Mathf.Sin(Mathf.Deg2Rad * index * angle));

        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angle); //x = R * sqrt(i) * cos(θi​)
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angle); //z = R * sqrt(i) * sin(θi​)

        return new Vector3(x, 0, z);
    }
}
