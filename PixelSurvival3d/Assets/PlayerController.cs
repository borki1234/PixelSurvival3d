using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float sprintSpeed = 10f;
    public float mouseSensitivity = 2f;
    public float jumpForce = 8f;
    public Camera playerCamera;

    private CharacterController characterController;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f;
    private bool isSprinting = false;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMovement();
        HandleMouseLook();

        // Check for user input to interact with the environment (e.g., left mouse button)
        if (Input.GetMouseButtonDown(0))
        {
            MineBlock();
        }
    }

    void HandleMovement()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalMovement, 0, verticalMovement).normalized;
        Vector3 moveVelocity;

        if (isSprinting && characterController.isGrounded)
        {
            moveVelocity = moveDirection * sprintSpeed;
        }
        else
        {
            moveVelocity = moveDirection * movementSpeed;
        }

        characterController.Move(transform.TransformDirection(moveVelocity) * Time.deltaTime);

        // Toggle sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        // Apply gravity
        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime);

        // Reset vertical velocity when grounded
        if (characterController.isGrounded)
        {
            verticalVelocity = -0.5f; // Prevents constant falling when grounded

            // Jumping
            if (Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void MineBlock()
    {
        // Raycast from the camera to detect the block
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the hit object has the MineableBlock script
            MineableBlock mineableBlock = hit.collider.GetComponent<MineableBlock>();

            // If the hit object is mineable, mine it
            if (mineableBlock != null)
            {
                mineableBlock.MineBlock();
            }
        }
    }
}
