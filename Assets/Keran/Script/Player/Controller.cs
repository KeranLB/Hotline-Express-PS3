
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Header("Set component :")]
    [SerializeField] public Rigidbody rb;
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform _targetCamera;
    [SerializeField] private Transform _holdPoint;
    private Grab _grab;
    private Inspect _inspect;
    [SerializeField] private AudioSource _grabAudio;
    private AudioSource _interactAudio;
    private AudioSource _inspectAudio;
    [SerializeField] private GameObject _pauseMenu;
    public Image tutoControl;

    [Header("Aim Point UI :")]
    public Image aimPoint;
    public Sprite grabSprite;
    [SerializeField] private Sprite _grabSpriteOuvert;
    [SerializeField] private Sprite _interactSprite;
    [SerializeField] private Sprite _inspectSprite;
    [SerializeField] private Sprite _noneSprite;

    [Header("Text Tuto UI Keyboard :")]
    [SerializeField] private Sprite _interactSpriteK;
    public Sprite inspectSpriteK;
    [SerializeField] private Sprite _startInspectSpriteK;
    [SerializeField] private Sprite _grabSpriteK;
    [SerializeField] private Sprite _lookSpriteK;
    public Sprite moveSpriteK;
    [SerializeField] private Sprite _crouchSpriteK;
    [SerializeField] private Sprite _tipToeSpriteK;

    private TextMeshPro _inspectText;
    private TextMeshPro _interactText;
    private TextMeshPro _grabText;

    [Header("Text Tuto UI GamePad :")]
    [SerializeField] private Sprite _interactSpriteG;
    public Sprite inspectSpriteG;
    [SerializeField] private Sprite _startInspectSpriteG;
    [SerializeField] private Sprite _grabSpriteG;
    [SerializeField] private Sprite _lookSpriteG;
    public Sprite moveSpriteG;
    [SerializeField] private Sprite _crouchSpriteG;
    [SerializeField] private Sprite _tipToeSpriteG;

    [Header("Control mapping :")]
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private InputActionReference _look;
    [SerializeField] private InputActionReference _move;
    [SerializeField] private InputActionReference _interact;
    [SerializeField] private InputActionReference _interactBis;
    [SerializeField] private InputActionReference _zoom;
    [SerializeField] private InputActionReference _tipToe;
    [SerializeField] private InputActionReference _crouch;
    [SerializeField] private InputActionReference _Pause;

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
    [SerializeField, Range(0, 1)] public float sensitivity;

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
    [HideInInspector] public bool isInPause = false;
    [HideInInspector] public bool isUsingGamepad;
    [HideInInspector] public bool isUsingKeyboard;
    private bool _isInStart = true;
    private bool _validCrouch = false;
    private bool _validTipToe = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        tutoControl.sprite = _crouchSpriteK;

        _targetDefaultPosY = _targetCamera.localPosition.y;
    }

    
    public void SetMoveUI()
    {
        StartCoroutine(Timer());
    }
    
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        tutoControl.enabled = false;
    }

    public void StartTuto()
    {
        if (_crouch.action.WasPressedThisFrame() && !_validCrouch)
        {
            _validCrouch = true;
        }

        if (_tipToe.action.WasPressedThisFrame() && !_validTipToe)
        {
            _validTipToe = true;
        }

        if (!_validCrouch && !_validTipToe)
        {
            if (isUsingGamepad)
            {
                tutoControl.sprite = _crouchSpriteG;
            }
            else if (isUsingKeyboard)
            {
                tutoControl.sprite = _crouchSpriteK;
            }
        }

        else if (_validCrouch && !_validTipToe)
        {
            if (isUsingGamepad)
            {
                tutoControl.sprite = _tipToeSpriteG;
            }
            else if (isUsingKeyboard)
            {
                tutoControl.sprite = _tipToeSpriteK;
            }
        }

        else if (_validCrouch && _validTipToe)
        {
            if (isUsingGamepad)
            {
                tutoControl.sprite = _lookSpriteG;
            }
            else if (isUsingKeyboard)
            {
                tutoControl.sprite = _lookSpriteK;
            }
            _isInStart = false;
        }
    }

    void Update()
    {
        if (_playerInput.currentControlScheme == "Gamepad")
        {
            isUsingGamepad = true;
            isUsingKeyboard = false;
        }
        else if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            isUsingKeyboard = true;
            isUsingGamepad = false;
        }

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
        if (_Pause.action.WasPressedThisFrame())
        {
            if (isInPause)
            {
                PauseMenu(false);
            }
            else
            {
                PauseMenu(true);
            }
        }
    }

    public void SensitivityChange(float value)
    {
        sensitivity = value;
    }

    public void PauseMenu(bool open)
    {
        if (open)
        {
            canMove = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            aimPoint.enabled = false;
            isInPause = true;
            _pauseMenu.SetActive(true);
        }
        else if (!open)
        {
            if (_inspect == null || !_inspect.isInspect)
            {
                canMove = true;
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            aimPoint.enabled = true;
            isInPause = false;
            _pauseMenu.SetActive(false);
        }
    }

    private void Look()
    {
        _lookDirection = _look.action.ReadValue<Vector3>();

        _verticalRotation -= _lookDirection.y * sensitivity;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -_maxVerticalLook, _maxVerticalLook);
        _targetCamera.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

        transform.Rotate(Vector3.up * _lookDirection.x * sensitivity);
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

        rb.AddForce(direction * _moveSpeed * _speedModification * 1000 * Time.deltaTime, ForceMode.Acceleration);
        rb.maxLinearVelocity = _moveSpeed * _speedModification;
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
            else if ((_grab == null || !_grab.isGrab))
            {
                aimPoint.sprite = _noneSprite;
                if (_isInStart)
                {
                    StartTuto();
                }
                else if (isInTuto)
                {
                    if (isUsingGamepad)
                    {
                        tutoControl.sprite = _lookSpriteG;
                    }
                    else if (isUsingKeyboard)
                    {
                        tutoControl.sprite = _lookSpriteK;
                    }
                }
            }
        }
        else if (_grab == null || !_grab.isGrab)
        {
            aimPoint.sprite = _noneSprite;
            if (_isInStart)
            {
                StartTuto();
            }
            else if (isInTuto)
            {
                if (isUsingGamepad)
                {
                    tutoControl.sprite = _lookSpriteG;
                }
                else if (isUsingKeyboard)
                {
                    tutoControl.sprite = _lookSpriteK;
                }
            }
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
                    if (isUsingKeyboard)
                    {
                        tutoControl.sprite = _interactSpriteK;
                    }
                    else if (isUsingGamepad)
                    {
                        tutoControl.sprite = _interactSpriteG;
                    }
                }
                if (_interact.action.WasPressedThisFrame())
                {
                    _grabAudio.Play();
                    interaction.Interact();
                }
                break;

            case ObjectType.Movable:
                _grab = target.GetComponent<Grab>();
                if (isInTuto)
                {
                    if (isUsingKeyboard)
                    {
                        tutoControl.sprite = _grabSpriteK;
                    }
                    else if (isUsingGamepad)
                    {
                        tutoControl.sprite = _grabSpriteG;
                    }
                }
                if (_grab == null || !_grab.isGrab)
                {
                    aimPoint.sprite = _grabSpriteOuvert;
                }
                if (_interact.action.WasPressedThisFrame())
                {
                    _grabAudio.Play();
                    _grab.MoveObject(_camera, _holdPoint, _interact, _look, _zoom, this);
                }
                break;

            case ObjectType.Inspectable:
                _inspect = target.GetComponent<Inspect>();
                
                if (isInTuto)
                {
                    if (isUsingKeyboard)
                    {
                        tutoControl.sprite = _startInspectSpriteK;
                    }
                    else if (isUsingGamepad)
                    {
                        tutoControl.sprite = _startInspectSpriteG;
                    }
                }
                aimPoint.sprite = _inspectSprite;
                if (canInspect && _interactBis.action.WasPressedThisFrame())
                {
                    _grabAudio.Play();
                    _inspect.StartInspect(_camera, _holdPoint, _look, _interact, _interactBis, this, distance);
                }
                break;
        }
    }
}