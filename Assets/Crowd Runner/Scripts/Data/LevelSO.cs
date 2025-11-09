using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "SO/Level", order = 0)]
public class LevelSO : ScriptableObject
{
    public Chunk[] chunks;
}
