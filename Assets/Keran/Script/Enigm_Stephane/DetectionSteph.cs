using Unity.VisualScripting;
using UnityEngine;

public class DetectionSteph : MonoBehaviour
{
    [SerializeField] private ObjectClass _objectClass;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _nbObjectWaited;
    private int _nbObject = 0;
    public bool isComplet = false;

    private void Verif()
    {
        _objectClass.interactType = ObjectType.Movable;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.useGravity = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stephane"))
        {
            Grab grab = other.gameObject.GetComponent<Grab>();
            grab.DropObject();
            other.transform.SetParent(transform);
            Vector3 target = other.gameObject.GetComponent<ObjectData>().target.transform.position;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
            other.gameObject.GetComponent<ObjectClass>().interactType = ObjectType.None;
            other.transform.position = target;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            _nbObject++;
            Debug.Log(_nbObject);
            Debug.Log(_nbObjectWaited);
            if (_nbObject == _nbObjectWaited)
            {
                Verif();
                isComplet = true;
            }
        }
    }
}
