using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionSteph : MonoBehaviour
{
    [Header("Set Components :")]
    [SerializeField] private ObjectClass _objectClass;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private BoxCollider _boxColliderClose;
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Mesh _meshOpen;
    [SerializeField] private Mesh _meshClose;
    [SerializeField] private Material _materialOpen;
    [SerializeField] private Material _materialClose;
    [SerializeField] private int _nbObjectWaited;
    private int _nbObject = 0;
    public bool isComplet = false;

    private void Start()
    {
        _boxColliderClose.enabled = false;
        _meshFilter.mesh = _meshOpen;
        _meshRenderer.material = _materialOpen;
    }

    private void Verif()
    {
        _objectClass.interactType = ObjectType.Movable;
        _rb.useGravity = true;
        _rb.isKinematic = false;
        _boxColliderClose.enabled = true;
        _meshFilter.mesh = _meshClose;
        _meshRenderer.material = _materialClose;
        _rb.constraints = RigidbodyConstraints.None;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stephane"))
        {
            Grab grab = other.gameObject.GetComponent<Grab>();
            grab.DropObject();
            other.gameObject.GetComponent<ObjectData>().target.transform.gameObject.SetActive(true);
            other.gameObject.SetActive(false);

            _nbObject++;

            if (_nbObject == _nbObjectWaited)
            {
                Verif();
                isComplet = true;
            }
        }
    }
}
