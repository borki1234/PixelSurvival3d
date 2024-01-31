using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject chunkPrefab;
    public int numChunksX = 2;
    public int numChunksY = 1;
    public int numChunksZ = 2;
    public float chunkSize = 16f; // Assuming each chunk is a cube for simplicity

    void Start()
    {
        GenerateChunks();
    }

    void GenerateChunks()
    {
        for (int x = 0; x < numChunksX; x++)
        {
            for (int y = 0; y < numChunksY; y++)
            {
                for (int z = 0; z < numChunksZ; z++)
                {
                    Vector3 chunkPosition = new Vector3(x * chunkSize, y * chunkSize, z * chunkSize);
                    Instantiate(chunkPrefab, chunkPosition, Quaternion.identity);
                }
            }
        }
    }
}
