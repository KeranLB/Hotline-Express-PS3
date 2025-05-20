using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour
{
    [SerializeField] public Rigidbody _rb;
    [HideInInspector] public bool isGrab;
    private Transform _holdPoint;

    private InputActionReference _interact;
    private InputActionReference _look;
    private InputActionReference _zoom;

    private Controller _controller;

    private float _speedComeBack = 10;
    private float _limitThrow = 30f;




    private void Update()
    {
        if (isGrab)
        {
            _controller.aimPoint.sprite = _controller.grabSprite;

            if (_interact.action.WasReleasedThisFrame())
            {
                DropObject();
            }

            else if (transform.position == _holdPoint.position)
            {
                _rb.linearVelocity = new Vector3(0,0,0);
            }
            else
            {
                _rb.AddForce(_holdPoint.position - transform.position, ForceMode.Impulse);
                _rb.maxLinearVelocity = _speedComeBack;
            }
        }
    }

    public void MoveObject(Transform parent, Transform holdPoint, InputActionReference interact, InputActionReference look, InputActionReference zoom, Controller controller)
    {
        _controller = controller;
        _look = look;
        _zoom = zoom;
        _controller.grab= this;
        transform.parent = parent;
        _holdPoint = holdPoint;
        _interact = interact;
        _rb.useGravity = false;
        _rb.freezeRotation = true;
        _rb.linearDamping = 10f;
        transform.Rotate(new Vector3(0f,0f,0f));
        transform.position = _holdPoint.position;
        
        isGrab = true;
    }

    public void DropObject()
    {
        transform.parent = null;

        Vector3 direction = _look.action.ReadValue<Vector3>();
        if (direction.x > _limitThrow || direction.x < _limitThrow || direction.y > _limitThrow || direction.y < -_limitThrow || direction.z > _limitThrow || direction.z < _limitThrow)
        {
            Mathf.Clamp(direction.x, -_speedComeBack, _speedComeBack);
            Mathf.Clamp(direction.y, -_speedComeBack, _speedComeBack);
            Mathf.Clamp(direction.z, -_speedComeBack, _speedComeBack);
            transform.eulerAngles = _holdPoint.eulerAngles;
            _rb.AddRelativeForce(direction, ForceMode.Impulse);
        }
        _rb.useGravity = true;
        _rb.freezeRotation = false;
        _rb.linearDamping = 1f;
        Debug.Log(_look.action.ReadValue<Vector3>());

        _rb.maxLinearVelocity = _speedComeBack;
        isGrab = false;
    }
}
