using Unity.VisualScripting;
using UnityEngine;

public class DetectionSteph : MonoBehaviour
{
    [SerializeField] private int _nbObjectWaited;
    private int _nbObject;
    public bool isComplet = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Stephane"))
        {
            other.transform.SetParent(transform);
            Vector3 target = other.gameObject.GetComponent<ObjectData>().target.transform.position;
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.isKinematic = true;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            other.gameObject.GetComponent<ObjectClass>().interactType = ObjectType.None;
            other.transform.position = target;
            _nbObject++;
            if(_nbObject == _nbObjectWaited)
            {
                isComplet = true;
            }
        }
    }
}
