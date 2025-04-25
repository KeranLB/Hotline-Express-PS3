using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private float _distanceZoom;
    [HideInInspector] public bool isGrab;
    public float moveSpeed = 10f;

    private InputActionReference _interact;
    private InputActionReference _zoom;

    private void Update()
    {
        if (isGrab)
        {
            //Zoom(_zoom.action.ReadValue<float>());
            if (_interact.action.WasReleasedThisFrame())
            {
                DropObject();
            }
        }
    }

    public void MoveObject(Transform parent, Transform holdPoint, InputActionReference interact, InputActionReference zoom)
    {
        transform.parent = parent;
        transform.position = holdPoint.position;
        _interact = interact;
        _zoom = zoom;
        _rb.useGravity = false;
        _rb.freezeRotation = true;
        _rb.linearDamping = 10f;
        transform.Rotate(new Vector3(0f,0f,0f));
        isGrab = true;
        Debug.Log("grab");
    }

    public void DropObject()
    {
        transform.parent = null;
        _rb.useGravity = true;
        _rb.freezeRotation = false;
        _rb.linearDamping = 1f;
        isGrab = false;
        Debug.Log("relache");
    }

    public void Zoom(float zoomValue)
    {
        if (transform.position.z < 3f && transform.position.z > 1f)
        {
            transform.position += new Vector3(0f, 0f, zoomValue/10f);
        }
    }
}
