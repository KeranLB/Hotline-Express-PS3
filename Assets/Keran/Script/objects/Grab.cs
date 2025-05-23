using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Grab : MonoBehaviour
{
    public Rigidbody rb;
    [HideInInspector] public bool isGrab;
    private Transform _holdPoint = null;

    private InputActionReference _interact;
    private InputActionReference _look;
    private InputActionReference _zoom;

    private Controller _controller;

    private float _speedComeBack = 10;
    private float _throwForce = 300f;
    private Vector3 _lastPosition;
    private int _lenghtList = 3;

    private List<Vector3> _lastPositions = new List<Vector3>();


    private void Start()
    {
        gameObject.TryGetComponent<Rigidbody>(out rb);
    }


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
                //_lastPosition = transform.position;
                rb.linearVelocity = new Vector3(0,0,0);
            }
            else
            {
                //_lastPosition = transform.position;
                rb.AddForce(_holdPoint.position - transform.position, ForceMode.Impulse);
                rb.maxLinearVelocity = _speedComeBack;
            }
        }
    }

    private void LateUpdate()
    {
        if (isGrab)
        {
            if (_lastPositions.Count < _lenghtList)
            {
                _lastPositions.Add(_holdPoint.position);
            }
            else
            {
                for (int i = 0; i < _lenghtList - 1; i++)
                {
                    _lastPositions[i] = _lastPositions[i+1];
                }
                _lastPositions[_lenghtList - 1] = _holdPoint.position;
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
        rb.useGravity = false;
        rb.freezeRotation = true;
        rb.linearDamping = 10f;
        transform.Rotate(new Vector3(0f,0f,0f));
        transform.position = _holdPoint.position;
        isGrab = true;
    }

    public void DropObject()
    {
        int i = _lenghtList - 1;
        bool flag = false;
        _lastPosition = _lastPositions[i];
        Debug.Log("holdPoint : " + _holdPoint.position);
        while (i >= 0 && !flag)
        {
            Debug.Log("iteration " + i + " " +_lastPositions[i]);
            if (_lastPositions[i] != _holdPoint.position)
            {
                Debug.Log("last position différente");
                _lastPosition = _lastPositions[i];
                flag = true;
            }
            i--;

        }
        Vector3 delta =  _holdPoint.position - _lastPosition;

        float x = Mathf.Clamp(delta.x, -_throwForce, _throwForce); 
        float y = Mathf.Clamp(delta.y, -_throwForce, _throwForce);
        float z = Mathf.Clamp(delta.z, -_throwForce, _throwForce);
        delta = new Vector3(x, y, z);

        rb.linearVelocity = new Vector3(0f,0f,0f);

        rb.AddForce(delta * _throwForce, ForceMode.Impulse);
        transform.parent = null;
        rb.useGravity = true;
        rb.freezeRotation = false;
        rb.linearDamping = 1f;

        rb.maxLinearVelocity = _speedComeBack;
        isGrab = false;
    }
}
