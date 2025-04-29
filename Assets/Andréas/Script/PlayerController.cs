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
    public float smoothTime = 0.05f;
    public float maxVerticalLook = 80f;

    private CharacterController controller;
    private Vector3 velocity;

    private float verticalRotation = 0f;

    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Lock de la souris
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
    }

    void Look()
    {
        // Input souris brut
        Vector2 targetMouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Lissage avec SmoothDamp pour une inertie douce
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, smoothTime);

        // Rotation verticale (haut/bas)
        verticalRotation -= currentMouseDelta.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalLook, maxVerticalLook);
        cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Rotation horizontale (gauche/droite) : le corps tourne ici
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Direction basée sur la caméra sans inclinaison verticale
        Vector3 camForward = cameraHolder.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = cameraHolder.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 move = camRight * moveX + camForward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Gravité
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}