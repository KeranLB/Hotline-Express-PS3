using UnityEngine;

public class PanelFinalManager : MonoBehaviour
{
    [SerializeField] private BoxCollider _colliderA;
    [SerializeField] private BoxCollider _colliderB;
    private int _nbObjectWaited = 3;
    private int _nbObject = 0;
    public bool isComplet = false;

    private void Start()
    {
        _colliderA.enabled = false;
        _colliderB.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Final"))
        {
            Grab grab = other.gameObject.GetComponent<Grab>();
            grab.DropObject();
            other.gameObject.GetComponent<ObjectData>().target.transform.gameObject.SetActive(true);
            other.gameObject.SetActive(false);

            _nbObject++;

            if (_nbObject == _nbObjectWaited)
            {
                isComplet = true;
                _colliderA.enabled = true;
                _colliderB.enabled = true;
            }
        }
    }
}
