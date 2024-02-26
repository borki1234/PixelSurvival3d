using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float gravity = -9.8f;
    private float verticalMomentum = 0;
    private bool isGrounded;

    private Transform player;
    private World world;
    public float playerWidth = 0.15f; // Adjust as necessary for enemy size

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        world = GameObject.FindObjectOfType<World>();

        // Ensure the world variable is not null before proceeding
        if (world == null)
        {
            Debug.LogError("Failed to find the World instance. Make sure there's a GameObject with a World component in the scene.");
        }
    }


    void Update()
    {
        CheckIfGrounded();
        ApplyGravity();

        if (isGrounded)
        {
            MoveTowardsPlayer();
        }
    }

    void CheckIfGrounded()
    {
        Vector3 position = transform.position;
        isGrounded = false;
        if (world.CheckForVoxel(new Vector3(position.x - playerWidth, position.y - 1f, position.z - playerWidth)) ||
            world.CheckForVoxel(new Vector3(position.x + playerWidth, position.y - 1f, position.z - playerWidth)) ||
            world.CheckForVoxel(new Vector3(position.x + playerWidth, position.y - 1f, position.z + playerWidth)) ||
            world.CheckForVoxel(new Vector3(position.x - playerWidth, position.y - 1f, position.z + playerWidth)))
        {
            isGrounded = true;
        }
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            verticalMomentum += gravity * Time.deltaTime;
            transform.Translate(Vector3.up * verticalMomentum * Time.deltaTime);
        }
        else
        {
            verticalMomentum = 0;
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        // Ensure the enemy moves only horizontally by zeroing the Y component
        direction.y = 0;
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }
}
