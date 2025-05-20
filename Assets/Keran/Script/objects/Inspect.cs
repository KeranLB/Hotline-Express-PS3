using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inspect : MonoBehaviour
{
    private InputActionReference _rotate;
    private InputActionReference _interact;
    private InputActionReference _release;

    private Controller _controller;

    private Vector3 _rotateValue;
    private Vector3 _originPosition;
    private Vector3 _originRotation;
    private Vector3 _originScale;

    private Transform _camera;

    [HideInInspector] public bool isInspect;
    private bool _canRelease;

    [Header("Object Collider :")]
    [SerializeField] private Collider _collider;

    [Header("atomic parameters :")]
    [SerializeField,Range(0,3)] private float _minDistance;
    [SerializeField, Range(0f, 10f)] private float _reduceSize;
    [SerializeField,Range(0,100)] private float _rotationSpeed;



    private void Start()
    {
        _originPosition = transform.position;
        _originRotation = transform.eulerAngles;
        _originScale = transform.localScale;
    }

    private void Update()
    {
        if (isInspect)
        {
            if (_interact.action.IsPressed())
            {
                ObjectRotation();
            }

            if (_canRelease && _release.action.WasPressedThisFrame())
            {
                StartCoroutine(StopInspect());
            }
        }
    }

    public void StartInspect(Transform camera, Transform holdPoint,InputActionReference rotation, InputActionReference interact, InputActionReference release, Controller controller, float distance)
    {
        _collider.enabled = false;
        transform.parent = camera;
        transform.position = holdPoint.position;
        transform.localScale = transform.localScale * _reduceSize;
        if (distance < _minDistance)
        {
            transform.parent.parent.localPosition -= transform.parent.parent.forward * (_minDistance - distance);
        }
        _camera = camera;
        _rotate = rotation;
        _interact = interact;
        _release = release;
        _controller = controller;
        isInspect = true;
        _controller.canMove = false;
        _controller.canInspect = false;
        _controller.aimPoint.enabled = false;
        StartCoroutine(DelayClick());
    }

    IEnumerator DelayClick()
    {
        yield return new WaitForEndOfFrame();
        _canRelease = true;
    }

    private void ObjectRotation()
    {
        _rotateValue = _rotate.action.ReadValue<Vector3>();

        float rotX = _rotateValue.y * _rotationSpeed * Time.deltaTime;
        float rotY = _rotateValue.x * _rotationSpeed * Time.deltaTime;

        // Rotation par rapport à l'orientation de la caméra
        transform.Rotate(_camera.right, rotX, Space.World);
        transform.Rotate(Vector3.up, rotY, Space.World); // Ici on garde un axe global Y pour éviter des dérives
    }

    IEnumerator StopInspect()
    {
        _controller.aimPoint.enabled = true;
        _collider.enabled = true;
        transform.parent = null;
        transform.eulerAngles = _originRotation;
        transform.position = _originPosition;
        transform.localScale = _originScale;
        isInspect = false;
        _canRelease = false;
        _controller.canMove = true;
        yield return new WaitForEndOfFrame();
        _controller.canInspect = true;
    }
}
