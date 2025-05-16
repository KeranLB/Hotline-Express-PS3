using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour
{
    [SerializeField] public Rigidbody _rb;
    [HideInInspector] public bool isGrab;
    private Transform _holdPoint;

    private InputActionReference _interact;

    private Controller _controller;

    public float speedComeBack;




    private void Update()
    {
        if (isGrab)
        {
            _controller.aimPoint.sprite = _controller.grabSprite;
            //transform.position = _holdPoint.position;
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
                _rb.maxLinearVelocity = speedComeBack;
            }
        }
    }

    public void MoveObject(Transform parent, Transform holdPoint, InputActionReference interact, Controller controller)
    {
        _controller = controller;
        _controller.grab= this;
        transform.parent = parent;
        _holdPoint = holdPoint;
        //_holdPoint.position += new Vector3(0f, 0f, _objectSize) * transform.forward;
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
        _rb.useGravity = true;
        _rb.freezeRotation = false;
        _rb.linearDamping = 1f;
        isGrab = false;
    }
}
