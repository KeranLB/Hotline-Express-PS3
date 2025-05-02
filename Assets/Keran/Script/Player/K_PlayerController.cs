using UnityEngine;
using UnityEngine.InputSystem;

public class K_PlayerController : MonoBehaviour
{
    [Header("Control mapping :")]
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _crouch;
    [SerializeField] private InputActionReference _tiptoe;

    [Header("Object set :")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Rigidbody _rb; // à enlever
    [SerializeField] private CharacterController controller;
    [SerializeField] private Canvas _safeCanvas;

    [Header("Paramètre atomique :")]
    [SerializeField, Range(0, 500)] private float _moveSpeed;
    [SerializeField, Range(0, 500)] private float _lookSpeed;

    [Header("Crouch & TipToe Settings :")]
    [SerializeField] private float crouchScale = 0.5f;
    [SerializeField] private float normalScale = 1f;
    [SerializeField] private float tiptoeOffset = 0.2f;

    [HideInInspector] public bool canMove = true;

    private Vector3 _moveDirection;
    private Vector3 _lookDirection;
    private Vector3 originalCameraPosition;
    private bool isCrouching = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _safeCanvas.enabled = false;

        originalCameraPosition = _camera.localPosition;

        _crouch.action.Enable();
        _tiptoe.action.Enable();
    }

    private void Update()
    {
        if (_interact.action.ReadValue<float>() == 1f)
        {
            Open();
        }
        else
        {
            close();
        }

        if (canMove)
        {
            _moveDirection = _move.action.ReadValue<Vector3>();
            _rb.MovePosition(transform.position + transform.forward + _moveDirection * _moveSpeed * Time.deltaTime);

            _lookDirection = _look.action.ReadValue<Vector3>();
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + _lookDirection.y * _lookSpeed, 0f);
            _camera.eulerAngles = new Vector3(_camera.eulerAngles.x + _lookDirection.x * _lookSpeed, transform.eulerAngles.y, 0f);

            HandleCrouch();
            HandleTiptoe();
        }
    }

    private void HandleCrouch()
    {
        bool crouchPressed = _crouch.action.ReadValue<float>() > 0.1f;

        if (crouchPressed && !isCrouching)
        {
            transform.localScale = new Vector3(1f, crouchScale, 1f);
            isCrouching = true;
        }
        else if (!crouchPressed && isCrouching)
        {
            transform.localScale = new Vector3(1f, normalScale, 1f);
            isCrouching = false;
        }
    }

    private void HandleTiptoe()
    {
        bool tiptoePressed = _tiptoe.action.ReadValue<float>() > 0.1f;

        if (tiptoePressed)
        {
            _camera.localPosition = originalCameraPosition + new Vector3(0f, tiptoeOffset, 0f);
        }
        else
        {
            _camera.localPosition = originalCameraPosition;
        }
    }

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