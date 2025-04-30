using UnityEngine;
using UnityEngine.InputSystem;

public class Inspect : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 100f;

    private InputActionReference _rotate;
    private InputActionReference _interact;
    private Transform _holdPoint;
    private Transform _originalParent;
    private Vector3 _originalPosition;
    private Quaternion _originalRotation;

    private Controller _playerController;

    private bool _isInspecting = false;

    private void Update()
    {
        if (_isInspecting)
        {
            RotateObject();

            if (_interact.action.WasReleasedThisFrame())
            {
                StopInspect();
            }
        }
    }
    private Transform _cameraTransform;

    public void StartInspect(Transform cameraTransform, Transform holdPoint, InputActionReference rotateAction, InputActionReference interactAction, Controller controller)
    {
        _cameraTransform = cameraTransform;
        _rotate = rotateAction;
        _interact = interactAction;
        _holdPoint = holdPoint;
        _playerController = controller;

        // Save state
        _originalParent = transform.parent;
        _originalPosition = transform.position;
        _originalRotation = transform.rotation;

        // Prepare object
        transform.parent = cameraTransform;
        transform.position = holdPoint.position;
        transform.rotation = Quaternion.identity;

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        _playerController.canMove = false;
        _isInspecting = true;
    }

    private void StopInspect()
    {
        transform.parent = _originalParent;
        transform.position = _originalPosition;
        transform.rotation = _originalRotation;

        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        _playerController.canMove = true;
        _isInspecting = false;
    }

    private void RotateObject()
    {
        Vector2 rotationInput = _rotate.action.ReadValue<Vector3>();
        float rotX = rotationInput.y * _rotationSpeed * Time.deltaTime;
        float rotY = -rotationInput.x * _rotationSpeed * Time.deltaTime;

        // Rotation par rapport à l'orientation de la caméra
        transform.Rotate(_cameraTransform.right, rotX, Space.World);
        transform.Rotate(Vector3.up, rotY, Space.World); // Ici on garde un axe global Y pour éviter des dérives
    }
}