using UnityEngine;

public class DetectionFollow : MonoBehaviour
{
    [SerializeField] Material _activeMaterial;
    [SerializeField] Material _unactiveMaterial;
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<MeshRenderer>().material = _activeMaterial;
    }

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<MeshRenderer>().material = _unactiveMaterial;
    }
}
