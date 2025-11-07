using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Vector3 size;

    public float GetLength() => size.z;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
