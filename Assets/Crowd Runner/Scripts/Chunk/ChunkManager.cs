using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager instance;

    [Header("Elements")]
    [SerializeField] private LevelSO[] levels;
    [SerializeField] private int chunkCount;
    [SerializeField] private GameObject finishLine;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        GenerateLevel();

        finishLine = GameObject.FindWithTag("Finish");

        //CreateRandomLevel();
    }

    private void GenerateLevel()
    {
        int currentLevel = GetLevel();


        currentLevel %= levels.Length;

        //Debug.Log("Current level: " + currentLevel);
        //Debug.Log("Levels length: " + levels.Length);

        LevelSO level = levels[currentLevel];

        CreateLevel(level.chunks);
    }


    private void CreateLevel(Chunk[] levelChunks)
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

    public float GetFinishZ() => finishLine.transform.position.z;

    public int GetLevel() => PlayerPrefs.GetInt("level", 0);

}
