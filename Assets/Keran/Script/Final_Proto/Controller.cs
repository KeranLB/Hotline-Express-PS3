using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Header("Set component :")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _targetCamera;
    [SerializeField] private Transform _holdPoint;
    [SerializeField] private Image _aimPoint;

    [Header("control mapping :")]
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _interactBis;
    [SerializeField] private InputActionReference _zoom;
    [SerializeField] private InputActionReference _tipToe;
    [SerializeField] private InputActionReference _crouch;

    private Vector3 _moveDirection;
    private Vector3 _lookDirection;

    [Header("Move Settings")]
    [SerializeField, Range(0, 500)] private float _moveSpeed;

    [Header("Look Settings")]
    [SerializeField, Range(0, 500)] private float _sensitivity;

    [Header("Raycast settings")]
    [SerializeField, Range(0, 500)] private float _rayDistance;

    private float _verticalRotation = 0f;
    private float _maxVerticalLook = 80f;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canInspect = true;
    public bool isLock = true;



    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canMove)
        {
            if (!isLock)
            {
                Move();
            }
            Look();
            RaycastThrow();
        }
    }

    private void Look()
    {
        // Input souris brut
        _lookDirection = _look.action.ReadValue<Vector3>();

        // Rotation verticale (haut/bas)
        _verticalRotation -= _lookDirection.y * _sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalLook, _maxVerticalLook);
        _targetCamera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        
        // Rotation horizontale (gauche/droite) : le corps tourne ici
        transform.Rotate(Vector3.up * _lookDirection.x * _sensitivity);
    }

    private void TipToe()
    {
        if (_tipToe.action.WasPressedThisFrame())
        {
            _camera.position += new Vector3(0f, 2f, 0f);
        }
        if (_tipToe.action.WasReleasedThisFrame())
        {
            _camera.position -= new Vector3(0f, 2f, 0f);

        }
    }

    private void Crouch()
    {
        if (_crouch.action.WasPressedThisFrame())
        {
            _camera.position -= new Vector3(0f, 2f, 0f);
        }
        if (_crouch.action.WasReleasedThisFrame())
        {
            _camera.position += new Vector3(0f, 2f, 0f);
        }
    }

    void Move()
    {
        _moveDirection = _move.action.ReadValue<Vector3>();

        Vector3 direction = _moveDirection.x * transform.right + transform.forward * _moveDirection.z;

        _rb.AddForce(direction * _moveSpeed);
        _rb.maxLinearVelocity = _moveSpeed;
    }

    private void RaycastThrow()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.position, _camera.forward, out hit, _rayDistance))
        {
            GameObject test = hit.collider.gameObject;
            if (test.GetComponent<ObjectClass>() != null)
            {
                ObjectClass objectClass = test.GetComponent<ObjectClass>();

                ObjectAction(test, objectClass.interactType, hit.distance);
            }
            else
            {
                _aimPoint.color = Color.white;
            }
        }
    }

    private void ObjectAction(GameObject target, ObjectType interactType, float distance)
    {
        switch (interactType)
        {
            case ObjectType.Interactable :
                Interaction interaction = target.GetComponent<Interaction>();
                _aimPoint.color = Color.green;
                if (_interact.action.WasPressedThisFrame())
                {
                    interaction.Interact();
                }
                break;

            case ObjectType.Movable :
                Grab grab = target.GetComponent<Grab>();
                _aimPoint.color = Color.red;
                if (_interact.action.WasPressedThisFrame())
                {
                    grab.MoveObject(_camera, _holdPoint, _interact, _zoom);
                }
                break;

            case ObjectType.Inspectable :
                Inspect inspect = target.GetComponent<Inspect>();
                _aimPoint.color = Color.blue;
                if (canInspect && _interactBis.action.WasPressedThisFrame())
                {
                    inspect.StartInspect(_camera, _holdPoint, _look, _interact, _interactBis, this, distance);
                }
                break;
        }
    }
}
