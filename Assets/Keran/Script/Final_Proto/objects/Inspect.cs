using UnityEngine;
using UnityEngine.InputSystem;

public class Inspect : MonoBehaviour
{
    private InputActionReference _rotate;
    private Vector3 _rotateValue;
    private InputActionReference _interact;
    private Controller _controller;
    [HideInInspector] public bool isInspect;
    public Vector3 _originPosition;

    [SerializeField] private float _rotationSpeed = 100f; // Vitesse de rotation configurable

    private void Start()
    {
        _originPosition = transform.position;
    }

    private void Update()
    {
        if (isInspect)
        {
            if (_interact.action.WasReleasedThisFrame())
            {
                StopInspect();
            }
            ObjectRotation();
        }
    }

    public void StartInspect(Transform parent, Transform holdPoint, InputActionReference rotation, InputActionReference interact, Controller controller)
    {
        transform.parent = parent;
        transform.position = holdPoint.position;
        _rotate = rotation;
        _interact = interact;
        isInspect = true;
        _controller = controller;
        _controller.canMove = false;

        // Geler la physique si besoin
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    private void ObjectRotation()
    {
        _rotateValue = _rotate.action.ReadValue<Vector3>(); // Attention : en général une rotation est Vector2 (X = souris horizontale, Y = souris verticale)

        // Appliquer une rotation inverse pour que les mouvements soient naturels
        float rotationX = -_rotateValue.y * _rotationSpeed * Time.deltaTime;
        float rotationY = _rotateValue.x * _rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.right, rotationX, Space.Self);
        transform.Rotate(Vector3.up, rotationY, Space.World);
    }

    private void StopInspect()
    {
        transform.parent = null;
        transform.position = _originPosition;
        isInspect = false;
        _controller.canMove = true;

        // Restaurer la physique si besoin
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
}