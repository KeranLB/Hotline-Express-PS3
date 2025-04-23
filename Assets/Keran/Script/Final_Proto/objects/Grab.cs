using Unity.VisualScripting;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    private Transform _holdPoint;
    private float _distanceZoom;
    [HideInInspector] public bool isGrab;
    public float moveSpeed = 10f;

    public void MoveObject(Transform holdPoint)
    {
        transform.parent = holdPoint;
        _rb.useGravity = false;
        _rb.linearDamping = 10f;
        isGrab = true;
        Debug.Log("grab");
    }

    public void DropObject(Transform holdPoint)
    {
        transform.parent = null;
        _rb.useGravity = true;
        _rb.linearDamping = 1f;
        isGrab = false;
        Debug.Log("relache");
    }

    public void Zoom(float zoomValue,Transform holdPoint)
    {
        holdPoint.position += new Vector3(0f,0f,zoomValue);
    }
}
