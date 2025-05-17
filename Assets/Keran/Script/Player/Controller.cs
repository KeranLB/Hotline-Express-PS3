using TMPro;
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
    public Grab grab;
    [SerializeField] private AudioSource _grabAudio;
    private AudioSource _interactAudio;
    private AudioSource _inspectAudio;

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
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _interactBis;
    [SerializeField] private InputActionReference _zoom;
    [SerializeField] private InputActionReference _tipToe;
    [SerializeField] private InputActionReference _crouch;

    [Header("TipToe :")]
    [SerializeField] private float _targetTipToePosY;
    [SerializeField] private float _targetCrouchPosY;
    [SerializeField] private float _targetDefaultPosY;
    private float _timeTranslateCam = 0f;
    [SerializeField] private float _speedTranslateCam;
    [SerializeField] private float _speedModification = 1f;

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

        _targetDefaultPosY = _targetCamera.localPosition.y;
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
            TipToeAndCrouch();
            RaycastThrow();
        }
    }

    private void Look()
    {
        if (_playerInput.currentControlScheme == "Gamepad")
        {
            _sensitivity = 0.25f; 
        }
        else if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            _sensitivity = 0.1f;
        }
        _lookDirection = _look.action.ReadValue<Vector3>();

        _verticalRotation -= _lookDirection.y * _sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalLook, _maxVerticalLook);
        _targetCamera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * _lookDirection.x * _sensitivity);
    }
    
    
    private void TipToeAndCrouch()
    {
        if (_tipToe.action.WasReleasedThisFrame() || _crouch.action.WasReleasedThisFrame())
        {
            _timeTranslateCam = 0f;
            _speedModification = 1f;
        }
        else if (_tipToe.action.IsPressed())
        {
            if (_targetCamera.localPosition.y != _targetTipToePosY)
            {
            CamTranslate(_targetCamera.localPosition.y, _targetTipToePosY);
            }
            _speedModification = 0.5f;
        }
        else if(_crouch.action.IsPressed())
        {
            if (_targetCamera.localPosition.y != _targetCrouchPosY)
            {
                CamTranslate(_targetCamera.localPosition.y, _targetCrouchPosY);
            }
            _speedModification = 0.5f;
        }
        else if (_targetCamera.localPosition.y != _targetDefaultPosY)
        {
            CamTranslate(_targetCamera.localPosition.y, _targetDefaultPosY);
        }
        else if(_targetCamera.localPosition.y == _targetDefaultPosY)
        {
            _timeTranslateCam = 0f;
        }
    }

    private void CamTranslate(float start, float end)
    {
        _timeTranslateCam += _speedTranslateCam * Time.deltaTime;
        _timeTranslateCam = Mathf.Clamp01(_timeTranslateCam);
        float tmpX = _targetCamera.localPosition.x;
        float tmpZ = _targetCamera.localPosition.z;
        _targetCamera.localPosition = Vector3.Lerp(new Vector3(tmpX,start,tmpZ), new Vector3(tmpX, end,tmpZ), _timeTranslateCam);
    }

    private void Move()
    {
        _moveDirection = _move.action.ReadValue<Vector3>();
        Vector3 direction = (_moveDirection.x * transform.right + transform.forward * _moveDirection.z);

        _rb.AddForce(direction * _moveSpeed * _speedModification * 1000 * Time.deltaTime, ForceMode.Acceleration);
        _rb.maxLinearVelocity = _moveSpeed * _speedModification;
    }

    private void RaycastThrow()
    {
        RaycastHit hit;

        if (Physics.Raycast(_camera.position, _camera.forward, out hit, _rayDistance, LayerMask.GetMask("Default") ,QueryTriggerInteraction.Ignore))
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
                    _grabAudio.Play();
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
                    _grabAudio.Play();
                    grab.MoveObject(_camera, _holdPoint, _interact, this);
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
                    _grabAudio.Play();
                    inspect.StartInspect(_camera, _holdPoint, _look, _interact, _interactBis, this, distance);
                }
                break;
        }
    }
}