using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("control mapping :")]
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;

    [Header("atomic Settings")]
    [SerializeField] private Rigidbody _rb;
    private float _gravity = -9.81f;
    [SerializeField, Range(0, 500)] private float _moveSpeed;
    [SerializeField, Range(0, 500)] private float _lookSpeed;
    public float smoothTime = 0.05f;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;

    [SerializeField] private CharacterController controller;

    [HideInInspector] public bool canMove = true;
    [SerializeField] private Canvas _safeCanvas;
    [SerializeField] private Transform _camera;
    private Vector3 _velocity;
    private Vector3 _currentMouseDelta;
    private Vector3 _currentMouseDeltaVelocity;
    private float _verticalRotation = 0f;
    public float maxVerticalLook = 80f;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _safeCanvas.enabled = false;
    }

    private void Update()
    {
        // --- à mettre dans un autre script ---

        if (_interact.action.IsPressed())
        {
            Open();
        }
        else
        {
            close();
        }
        // --- ---

    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            Look();
            Move();
            //SimGravity();
        }
    }
    /*
    private void Look()
    {
        _lookDirection = _look.action.ReadValue<Vector3>();
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + _lookDirection.y * _lookSpeed, 0f);
        _camera.eulerAngles = new Vector3(_camera.eulerAngles.x + _lookDirection.x * _lookSpeed, transform.eulerAngles.y, 0f);
    }
    */

    private void Look()
    {
        // Input souris brut
        _lookDirection = _look.action.ReadValue<Vector3>();

        /*
        // Lissage avec SmoothDamp pour une inertie douce
        _currentMouseDelta = Vector3.SmoothDamp(_currentMouseDelta, _lookDirection, ref _currentMouseDeltaVelocity, smoothTime);
        */
        // Rotation verticale (haut/bas)
        _verticalRotation -= _lookDirection.y * _lookSpeed;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -maxVerticalLook, maxVerticalLook);
        _camera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        
        // Rotation horizontale (gauche/droite) : le corps tourne ici
        transform.Rotate(Vector3.up * _lookDirection.x * _lookSpeed);
    }
    void Move()
    {
        _moveDirection = _move.action.ReadValue<Vector3>();
        
        Vector3 camForward = _camera.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = _camera.right;
        camRight.y = 0f;
        camRight.Normalize();

        Vector3 move = camRight * _moveDirection.x + camForward * _moveDirection.z;
        
        controller.Move(move * _moveSpeed * Time.deltaTime);
        /*
        //Vector3 forward = new Vector3(_moveDirection.x + transform.localPosition.x, 0f, _moveDirection.z + transform.localPosition.z);
        Debug.Log(_moveDirection.z);
        Debug.Log(_moveDirection.x);
        _rb.MovePosition(transform.localPosition + (transform.forward * _moveDirection.z) + (transform.right * _moveDirection.x) * Time.deltaTime * _moveSpeed);
        */
    }

    void SimGravity()
    {
        if (controller.isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += _gravity * Time.deltaTime;
        controller.Move(_velocity * Time.deltaTime);
    }

    // partie à mettre dans un autre script
    private void Open()
    {
        canMove = false;
        _safeCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void close()
    {
        canMove = true;
        _safeCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
