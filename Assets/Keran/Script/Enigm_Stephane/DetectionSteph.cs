using Unity.VisualScripting;
using UnityEngine;

public class DetectionSteph : MonoBehaviour
{
    [SerializeField] private ObjectClass _objectClass;
    private Rigidbody _rb;
    [SerializeField] private int _nbObjectWaited;
    private int _nbObject = 0;
    public bool isComplet = false;

    private void Verif()
    {
        _objectClass.interactType = ObjectType.Movable;
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;
        _rb.isKinematic = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stephane"))
        {
            Grab grab = other.gameObject.GetComponent<Grab>();
            Vector3 target = other.gameObject.GetComponent<ObjectData>().target.transform.position;
            grab.DropObject();
            other.transform.position = target;
            other.gameObject.GetComponent<ObjectClass>().interactType = ObjectType.None;
            other.transform.SetParent(transform);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            _nbObject++;

            if (_nbObject == _nbObjectWaited)
            {
                Verif();
                isComplet = true;
            }
        }
    }
}
