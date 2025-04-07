using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(0,500)] private float _moveSpeed;
    

    private Vector3 _moveDirection;
    [SerializeField] private InputActionReference _move;

    private Vector3 _lookDirection;
    [SerializeField] private InputActionReference _look;
    [SerializeField] private Transform _camera;
    [SerializeField, Range(0, 500)] private float _lookSpeed;

    [SerializeField] private Rigidbody _rb;
    

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void Update()
    {
        _moveDirection = _move.action.ReadValue<Vector3>();
        //transform.forward += new Vector3 (0f,0f,_moveDirection.z * _moveSpeed);
        //transform.position = new Vector3(transform.right + _moveDirection.x * _moveSpeed, _moveDirection.y, 0f);
        _rb.MovePosition(transform.localPosition + _moveDirection * _moveSpeed * Time.deltaTime);
        
        //_rb.linearVelocity = new Vector3(_moveDirection.x, 0f,_moveDirection.z) * _moveSpeed;

        _lookDirection = _look.action.ReadValue<Vector3>();
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + _lookDirection.y * _lookSpeed, 0f);
        _camera.eulerAngles = new Vector3( _camera.eulerAngles.x + _lookDirection.x * _lookSpeed, transform.eulerAngles.y, 0f);
    }
}
