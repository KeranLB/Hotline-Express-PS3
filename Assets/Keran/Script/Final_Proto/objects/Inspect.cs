using UnityEngine;
using UnityEngine.InputSystem;

public class Inspect : MonoBehaviour
{
    private InputActionReference _rotate;
    private InputActionReference _interact;
    private Controller _controller;
    [HideInInspector] public bool isInspect;
    public Vector3 _originPosition;

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

    public void StartInspect(Transform parent, Transform holdPoint,InputActionReference rotation, InputActionReference interact, Controller controller)
    {
        transform.parent = parent;
        transform.position = holdPoint.position;
        _rotate = rotation;
        _interact = interact;
        isInspect = true;
        _controller = controller;
        _controller.canMove = false;
    }

    private void ObjectRotation()
    {

    }

    private void StopInspect()
    {
        transform.parent = null;
        transform.position = _originPosition;
        isInspect = false;
        _controller.canMove = true;
    }
}
