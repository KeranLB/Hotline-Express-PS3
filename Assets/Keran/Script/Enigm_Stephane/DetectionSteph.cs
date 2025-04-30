using Unity.VisualScripting;
using UnityEngine;

public class DetectionSteph : MonoBehaviour
{
    [SerializeField] private ObjectClass _objectClass;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _nbObjectWaited;
    private int _nbObject;
    public bool isComplet = false;

    private void Verif()
    {
        if (isComplet)
        {
            _objectClass.interactType = ObjectType.Movable;
            _rb.constraints = RigidbodyConstraints.None;
            _rb.useGravity = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Stephane"))
        {
            other.transform.SetParent(transform);
            Vector3 target = other.gameObject.GetComponent<ObjectData>().target.transform.position;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = false;
            other.gameObject.GetComponent<ObjectClass>().interactType = ObjectType.None;
            other.transform.position = target;
            _nbObject++;
            if(_nbObject == _nbObjectWaited)
            {
                isComplet = true;
            }
            Verif();
        }
    }
}
