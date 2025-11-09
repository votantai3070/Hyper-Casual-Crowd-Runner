using System;
using UnityEngine;

public enum EnemyState { Idle, Running }

public class Enemy : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    [SerializeField] private EnemyState enemyState;
    private Transform targetRunner;

    private void Update()
    {
        Debug.Log("targetRunner: " + targetRunner);

        ManageState();
    }

    private void ManageState()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                SearchTarget();
                break;

            case EnemyState.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void RunTowardsTarget()
    {
        if (targetRunner == null)
        {
            SearchTarget();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetRunner.position) < .5f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }

    private void StartRunningToTarget()
    {
        enemyState = EnemyState.Running;
        GetComponentInChildren<Animator>().Play("Run");
    }

    private void SearchTarget()
    {
        Collider[] detectedCollider = Physics.OverlapSphere(transform.position, searchRadius);

        Debug.Log("detectedCollider: " + detectedCollider.Length);

        float minDistance = float.MaxValue;

        foreach (var target in detectedCollider)
        {
            if (target.TryGetComponent(out Runner runner))
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    targetRunner = target.transform;
                }

                StartRunningToTarget();
            }
        }
    }
}
