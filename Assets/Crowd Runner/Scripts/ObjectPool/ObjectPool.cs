using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField] private int poolSize = 5;

    Dictionary<GameObject, Queue<GameObject>> poolDict = new();
    //[SerializeField] private GameObject runnerPrefab;
    //[SerializeField] private Transform runnersParent;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        //InitializeNewPool(runnerPrefab, runnersParent);
    }

    private void InitializeNewPool(GameObject prefab, Transform parentTransform = null)
    {
        poolDict[prefab] = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObject(prefab, parentTransform);
        }
    }
    private void CreateNewObject(GameObject prefab, Transform parentTransform = null)
    {
        GameObject newObj = Instantiate(prefab, transform);

        var pooled = newObj.GetComponent<PooledObject>();
        if (pooled == null)
            pooled = newObj.AddComponent<PooledObject>();
        pooled.originalPrefab = prefab;

        newObj.SetActive(false);

        poolDict[prefab].Enqueue(newObj);
    }

    public GameObject GetObject(GameObject prefab, Transform parentTransform = null)
    {
        if (!poolDict.ContainsKey(prefab))
            InitializeNewPool(prefab, parentTransform);

        if (poolDict[prefab].Count == 0)
            CreateNewObject(prefab, parentTransform);


        GameObject objectToGet = poolDict[prefab].Dequeue();

        objectToGet.SetActive(true);
        objectToGet.transform.parent = parentTransform;

        return objectToGet;
    }

    #region Return To Pool

    public void ReturnToPool(GameObject objectToReturn)
    {
        GameObject originalPrefab = objectToReturn.GetComponent<PooledObject>().originalPrefab;

        objectToReturn.SetActive(false);
        objectToReturn.transform.parent = transform;

        poolDict[originalPrefab].Enqueue(objectToReturn);
    }

    public void DelayReturnToPool(GameObject objectToReturn, float delay = .0001f)
    {
        StartCoroutine(DelayReturn(delay, objectToReturn));
    }

    private IEnumerator DelayReturn(float delay, GameObject objectToReturn)
    {
        yield return new WaitForSeconds(delay);

        ReturnToPool(objectToReturn);
    }

    #endregion
}
