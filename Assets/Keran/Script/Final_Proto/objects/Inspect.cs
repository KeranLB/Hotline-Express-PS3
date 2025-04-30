using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inspect : MonoBehaviour
{
    private InputActionReference _rotate;
    private Vector3 _rotateValue;
    private InputActionReference _interact;
    private InputActionReference _release;
    private Controller _controller;
    [HideInInspector] public bool isInspect;
    public Vector3 _originPosition;
    private Vector3 _originRotation;

    [SerializeField] private float _minDistance;

    [SerializeField,Range(0,500)] private float _rotationSpeed;
    private Transform _camera;
    private bool _canRelease;

    private void Start()
    {
        _originPosition = transform.position;
        _originRotation = transform.eulerAngles;
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
        transform.parent = camera;
        transform.position = holdPoint.position;
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
        transform.parent = null;
        transform.eulerAngles = _originRotation;
        transform.position = _originPosition;
        isInspect = false;
        _canRelease = false;
        _controller.canMove = true;
        yield return new WaitForEndOfFrame();
    }
}
