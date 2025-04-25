using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Set component :")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _holdPoint;

    [Header("control mapping :")]
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _zoom;

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




    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canMove)
        {
            Look();
            Move();
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
        _camera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);
        
        // Rotation horizontale (gauche/droite) : le corps tourne ici
        transform.Rotate(Vector3.up * _lookDirection.x * _sensitivity);
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
        
        _controller.Move(move * _moveSpeed * Time.deltaTime);
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

                // modification UI et outline

                ObjectAction(test, objectClass.interactType);
            }
        }
    }

    private void ObjectAction(GameObject target, ObjectType interactType)
    {
        switch (interactType)
        {
            case ObjectType.Interactable :
                Interaction interaction = target.GetComponent<Interaction>();
                if (_interact.action.WasPressedThisFrame())
                {
                    interaction.Interact();
                }
                break;

            case ObjectType.Movable :
                Grab grab = target.GetComponent<Grab>();
                if (_interact.action.WasPressedThisFrame())
                {
                    grab.MoveObject(_camera, _holdPoint, _interact, _zoom);
                }
                break;

            case ObjectType.Inspectable :
                Inspect inspect = target.GetComponent<Inspect>();
                if (_interact.action.WasPressedThisFrame())
                {
                    inspect.StartInspect(_camera, _holdPoint, _look, _interact, this);
                }
                else if (_interact.action.WasReleasedThisFrame())
                {
                    //Grab.DropObject();
                }
                break;
        }
    }
}
