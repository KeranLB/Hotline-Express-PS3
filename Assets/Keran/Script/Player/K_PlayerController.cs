using UnityEngine;
using UnityEngine.InputSystem;

public class K_PlayerController : MonoBehaviour
{
    [Header("control mapping :")]
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;


    [Header("object set :")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Rigidbody _rb; // à enlever
    [SerializeField] private CharacterController controller;
    [SerializeField] private Canvas _safeCanvas;

    [Header("parametre atomique :")]
    [SerializeField, Range(0,500)] private float _moveSpeed;
    [SerializeField, Range(0, 500)] private float _lookSpeed;

    [HideInInspector] public bool canMove = true;
    private Vector3 _moveDirection;
    private Vector3 _lookDirection;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _safeCanvas.enabled = false;
    }
    public void Update()
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
            //transform.forward += new Vector3 (0f,0f,_moveDirection.z * _moveSpeed);
            //transform.position = new Vector3(transform.right + _moveDirection.x * _moveSpeed, _moveDirection.y, 0f);
            _rb.MovePosition(transform.position+transform.forward + _moveDirection * _moveSpeed * Time.deltaTime);

            //_rb.linearVelocity = new Vector3(_moveDirection.x, 0f,_moveDirection.z) * _moveSpeed;

            _lookDirection = _look.action.ReadValue<Vector3>();
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + _lookDirection.y * _lookSpeed, 0f);
            _camera.eulerAngles = new Vector3(_camera.eulerAngles.x + _lookDirection.x * _lookSpeed, transform.eulerAngles.y, 0f);
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
