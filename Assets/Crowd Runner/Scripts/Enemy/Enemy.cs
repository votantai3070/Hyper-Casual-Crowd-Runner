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
    private Collider[] detectedCollider;

    private void Start()
    {
        detectedCollider = new Collider[100];
    }

    private void Update()
    {
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
    private void SearchTarget()
    {
        int count = Physics.OverlapSphereNonAlloc(transform.position, searchRadius, detectedCollider);

        Debug.Log("Count: " + count);

        float minDistance = float.MaxValue;
        Transform nearestTarget = null;

        for (int i = 0; i < count; i++)
        {
            if (detectedCollider[i].TryGetComponent(out Runner runner))
            {
                float distance = Vector3.Distance(transform.position, runner.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = runner.transform;
                }
            }

            if (nearestTarget != null)
            {
                targetRunner = nearestTarget;
                StartRunningToTarget();
            }
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
    }

    private void StartRunningToTarget()
    {
        enemyState = EnemyState.Running;
        GetComponentInChildren<Animator>().Play("Run");
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Runner>(out var runner))
        {
            SoundManager.instance.SetSoundEffect(SoundEffect.RunnerDie);

            ObjectPool.instance.DelayReturnToPool(runner.transform.parent.gameObject);
            ObjectPool.instance.DelayReturnToPool(gameObject);
        }
    }
}
