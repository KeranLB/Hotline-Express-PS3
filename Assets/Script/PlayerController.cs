using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Mouse Look Settings")]
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float maxVerticalLook = 80f;
    public float maxHorizontalLook = 90f;

    private CharacterController controller;
    private Vector3 velocity;

    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Look();
    }

   void Move()
{
    float moveX = Input.GetAxis("Horizontal");
    float moveZ = Input.GetAxis("Vertical");

    // Direction basée sur la caméra, mais sans inclinaison verticale
    Vector3 camForward = cameraHolder.forward;
    camForward.y = 0f;
    camForward.Normalize();

    Vector3 camRight = cameraHolder.right;
    camRight.y = 0f;
    camRight.Normalize();

    Vector3 move = camRight * moveX + camForward * moveZ;
    controller.Move(move * moveSpeed * Time.deltaTime);

    // Gravity
    if (controller.isGrounded && velocity.y < 0)
    {
        velocity.y = -2f;
    }

    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        // Rotation verticale (haut/bas)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalLook, maxVerticalLook);

        // Rotation horizontale (gauche/droite)
        horizontalRotation += mouseX;
        horizontalRotation = Mathf.Clamp(horizontalRotation, -maxHorizontalLook, maxHorizontalLook);

        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
    }
}