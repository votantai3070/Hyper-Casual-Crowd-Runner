using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Chunk[] chunkPrefab;
    [SerializeField] private Chunk[] levelChunks;
    [SerializeField] private int chunkCount;

    private void Start()
    {
        CreateRandomLevel();
    }

    private void CreateRandomLevel()
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < chunkCount; i++)
        {
            Chunk chunkRandom = chunkPrefab[Random.Range(0, chunkPrefab.Length)];

            if (i > 0)
                chunkPosition.z += chunkRandom.GetLength() / 2;

            Chunk chunkInstance = Instantiate(chunkRandom, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }

    private void CreateOrderLevel()
    {
        Vector3 chunkPosition = Vector3.zero;

        for (int i = 0; i < levelChunks.Length; i++)
        {
            Chunk chunkRandom = levelChunks[i];

            if (i > 0)
                chunkPosition.z += chunkRandom.GetLength() / 2;

            Chunk chunkInstance = Instantiate(chunkRandom, chunkPosition, Quaternion.identity, transform);

            chunkPosition.z += chunkInstance.GetLength() / 2;
        }
    }
}
