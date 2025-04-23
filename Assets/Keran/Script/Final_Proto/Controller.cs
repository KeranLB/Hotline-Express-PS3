using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Set component :")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private CharacterController controller;
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
        RaycastHit hit;

        if (Physics.Raycast(_camera.position, _camera.forward, out hit, _rayDistance))
        {
            GameObject test = hit.collider.gameObject;
            if (test.GetComponent<ObjectClass>() != null)
            {
                ObjectClass objectClass = test.GetComponent<ObjectClass>();

                // modification UI et outline

                ObjectAction(test, objectClass.interactType);
                if (objectClass.interactType == InteractType.Interactable)
                {
                    Debug.Log("Ca marche !!!!!!");
                    //objectClass.Interact();
                }
            }
        }

        Debug.Log(_zoom.action.ReadValue<float>());

        if (canMove)
        {
            Look();
            Move();
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
        
        controller.Move(move * _moveSpeed * Time.deltaTime);
    }

    private void ObjectAction(GameObject target, InteractType interactType)
    {
        switch (interactType)
        {
            case InteractType.Interactable :
                Interactable interactable = target.GetComponent<Interactable>();
                if (_interact.action.WasPressedThisFrame())
                {
                    //Grab.MoveObject();
                }
                break;

            case InteractType.Movable :
                Grab grab = target.GetComponent<Grab>();
                /*
                if (grab.isGrab)
                {
                    grab.Zoom(_zoom.action.ReadValue<float>(), _holdPoint);
                }
                */
                if (_interact.action.WasPressedThisFrame())
                {
                    grab.MoveObject(_holdPoint);
                }
                else if (_interact.action.WasReleasedThisFrame())
                {
                    grab.DropObject(_holdPoint);
                }
                break;

            case InteractType.Inspectable :
                Inspect inspect = target.GetComponent<Inspect>();
                if (_interact.action.WasPressedThisFrame())
                {
                    //Grab.MoveObject();
                }
                else if (_interact.action.WasReleasedThisFrame())
                {
                    //Grab.DropObject();
                }
                break;
        }
    }
}
