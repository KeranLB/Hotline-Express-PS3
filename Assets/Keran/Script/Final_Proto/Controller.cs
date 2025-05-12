using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [System.Serializable]
    public struct StanceData
    {
        public string name;
        public float cameraHeight;
        public float playerScaleY;
        public float speedMultiplier;
    }

    [Header("Set component :")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _targetCamera;
    [SerializeField] private Transform _holdPoint;
    public Grab grab;

    [Header("Aim Point UI :")]
    public Image aimPoint;
    public Sprite grabSprite;
    [SerializeField] private Sprite _grabSpriteOuvert;
    [SerializeField] private Sprite _interactSprite;
    [SerializeField] private Sprite _inspectSprite;
    [SerializeField] private Sprite _noneSprite;

    [Header("Text Tuto UI :")]
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _inspectText;
    [SerializeField] private TextMeshProUGUI _grabText;

    [Header("Control mapping :")]
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _interactBis;
    [SerializeField] private InputActionReference _zoom;
    [SerializeField] private InputActionReference _tipToe;
    [SerializeField] private InputActionReference _crouch;

    [Header("Stance Settings")]
    [SerializeField] private StanceData defaultStance = new StanceData { name = "Default", cameraHeight = 1.75f, playerScaleY = 1.0f, speedMultiplier = 1.0f };
    [SerializeField] private StanceData crouchStance = new StanceData { name = "Crouch", cameraHeight = 1.0f, playerScaleY = 0.35f, speedMultiplier = 0.4f };
    [SerializeField] private StanceData tipToeStance = new StanceData { name = "TipToe", cameraHeight = 2.2f, playerScaleY = 1.3f, speedMultiplier = 0.9f };
    [SerializeField] private float camLerpSpeed = 5f;
    [SerializeField] private float scaleLerpSpeed = 5f;

    private StanceData _currentStance;

    [Header("Move Settings")]
    [SerializeField, Range(0, 500)] private float _moveSpeed;

    [Header("Look Settings")]
    [SerializeField, Range(0, 500)] private float _sensitivity;

    [Header("Raycast settings")]
    [SerializeField, Range(0, 500)] private float _rayDistance;

    private Vector3 _moveDirection;
    private Vector3 _lookDirection;
    private float _verticalRotation = 0f;
    private float _maxVerticalLook = 80f;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool canInspect = true;
    [HideInInspector] public bool isLock = true;
    [HideInInspector] public bool isInTuto = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _grabText.enabled = false;
        _inspectText.enabled = false;
        _interactText.enabled = false;

        _currentStance = defaultStance;

        SetInitialCameraPosition();
    }

    private void SetInitialCameraPosition()
    {
        Vector3 camLocalPos = _camera.localPosition;
        camLocalPos.y = _currentStance.cameraHeight;
        _camera.localPosition = camLocalPos;
    }

    void Update()
    {
        if (canMove)
        {
            if (!isLock)
            {
                Move();
            }
            Look();
            HandleStance();
            RaycastThrow();
        }
    }

    private void Look()
    {
        _lookDirection = _look.action.ReadValue<Vector3>();

        _verticalRotation -= _lookDirection.y * _sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalLook, _maxVerticalLook);
        _targetCamera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * _lookDirection.x * _sensitivity);
    }

    private void HandleStance()
    {
        StanceData targetStance = defaultStance;

        if (_tipToe.action.IsPressed())
        {
            targetStance = tipToeStance;
        }
        else if (_crouch.action.IsPressed())
        {
            targetStance = crouchStance;
        }

        _currentStance = targetStance;

        // Smooth camera position
        Vector3 camPos = _camera.localPosition;
        camPos.y = Mathf.Lerp(camPos.y, _currentStance.cameraHeight, Time.deltaTime * camLerpSpeed);
        _camera.localPosition = camPos;

        // Smooth scale transition
        Vector3 scale = transform.localScale;
        float targetY = _currentStance.playerScaleY;
        scale.y = Mathf.Lerp(scale.y, targetY, Time.deltaTime * scaleLerpSpeed);
        transform.localScale = new Vector3(scale.x, scale.y, scale.z);
    }

    private void Move()
    {
        _moveDirection = _move.action.ReadValue<Vector3>();
        Vector3 direction = (_moveDirection.x * transform.right + transform.forward * _moveDirection.z);

        float adjustedSpeed = _moveSpeed * _currentStance.speedMultiplier;
        _rb.AddForce(direction * adjustedSpeed * 1000 * Time.deltaTime, ForceMode.Acceleration);
        _rb.maxLinearVelocity = adjustedSpeed;
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
            else if (!grab.isGrab)
            {
                _grabText.enabled = false;
                _interactText.enabled = false;
                _inspectText.enabled = false;
                aimPoint.sprite = _noneSprite;
            }
        }
        else if (!grab.isGrab)
        {
            _grabText.enabled = false;
            _interactText.enabled = false;
            _inspectText.enabled = false;
            aimPoint.sprite = _noneSprite;
        }
    }

    private void ObjectAction(GameObject target, ObjectType interactType, float distance)
    {
        switch (interactType)
        {
            case ObjectType.Interactable:
                Interaction interaction = target.GetComponent<Interaction>();
                aimPoint.sprite = _interactSprite;
                if (isInTuto)
                {
                    _interactText.enabled = true;
                    _inspectText.enabled = false;
                    _grabText.enabled = false;
                }
                if (_interact.action.WasPressedThisFrame())
                {
                    interaction.Interact();
                }
                break;

            case ObjectType.Movable:
                Grab grab = target.GetComponent<Grab>();
                if (isInTuto)
                {
                    _grabText.enabled = true;
                    _inspectText.enabled = false;
                    _interactText.enabled = false;
                }
                if (!grab.isGrab)
                {
                    aimPoint.sprite = _grabSpriteOuvert;
                }
                if (_interact.action.WasPressedThisFrame())
                {
                    grab.MoveObject(_camera, _holdPoint, _interact, _zoom, this);
                }
                break;

            case ObjectType.Inspectable:
                Inspect inspect = target.GetComponent<Inspect>();
                if (isInTuto)
                {
                    _inspectText.enabled = true;
                    _interactText.enabled = false;
                    _grabText.enabled = false;
                }
                aimPoint.sprite = _inspectSprite;
                if (canInspect && _interactBis.action.WasPressedThisFrame())
                {
                    inspect.StartInspect(_camera, _holdPoint, _look, _interact, _interactBis, this, distance);
                }
                break;
        }
    }
}