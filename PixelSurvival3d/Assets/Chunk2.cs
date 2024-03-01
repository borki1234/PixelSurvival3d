using UnityEngine;

public class Chunk2 : MonoBehaviour
{
    public GameObject blockPrefab;
    public int chunkSize = 16; // Assuming a cubic chunk
    public float blockSize = 1f;

    void Start()
    {
        GenerateBlocks();
    }

    void GenerateBlocks()
    {
        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    Vector3 blockPosition = new Vector3(x * blockSize, y * blockSize, z * blockSize);
                    Instantiate(blockPrefab, blockPosition, Quaternion.identity, transform);
                }
            }
        }
    }
}
