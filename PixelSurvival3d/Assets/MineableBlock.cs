using UnityEngine;

public class MineableBlock : MonoBehaviour
{
    public int health = 1;  // The health of the block before it breaks

    void Update()
    {
        // Check for user input (e.g., mouse click) to mine the block


    }

    public void MineBlock()  // Make the method public
    {
        // Decrease health when mining
        health--;

        // Check if the block is destroyed
        if (health <= 0)
        {
            // Call a method to handle the block being destroyed
            DestroyBlock();
        }
    }

    void DestroyBlock()
    {
        // Implement any additional effects or actions when the block is destroyed
        // For example, instantiate particle effects, play sound, etc.

        // Remove the block from the scene
        Destroy(gameObject);
    }
}
