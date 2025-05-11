using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionSteph : MonoBehaviour
{
    [Header("Set Components :")]
    [SerializeField] private ObjectClass _objectClass;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private int _nbObjectWaited;
    private int _nbObject = 0;
    public bool isComplet = false;

    private void Verif()
    {
        _objectClass.interactType = ObjectType.Movable;
        _rb.useGravity = true;
        _rb.isKinematic = false;
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
