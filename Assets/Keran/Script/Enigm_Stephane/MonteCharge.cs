using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonteCharge : MonoBehaviour
{
    [SerializeField] private Transform _openPoint;
    [SerializeField] private Transform _closedPoint;

    [SerializeField] private GameObject _rouage;
    private GameObject _box;
    [SerializeField] private CodeManager _codeManager;

    [SerializeField] private float _speedTravel;
    [SerializeField] private GameObject _door;
    private bool _asSwitch = false;

    private bool _boxIsIn = false;
    [HideInInspector] public bool valide = false;
    private float poseZ;

    private void Start()
    {
        _door.transform.position = _openPoint.position;
        poseZ = _door.transform.position.z;
    }
    public void closeDoor()
    {
        if (_boxIsIn && _codeManager.isCorrect)
        {
            valide = true;
            StartCoroutine(Travel(_openPoint, _closedPoint));
        }
    }

    private void SwitchItem()
    {
        _box.SetActive(false);
        _rouage.SetActive(true);
        _asSwitch = true;
        StartCoroutine(Travel(_closedPoint, _openPoint));
    }

    IEnumerator Travel(Transform startPoint, Transform endPoint)
    {
        Debug.Log("est dans la coroutine");
        float t = 0f;
        while (t < 1f)
        {
            t += _speedTravel * Time.deltaTime;
            t = Mathf.Clamp01(t);
            _door.transform.position = Vector2.Lerp(startPoint.position, endPoint.position, t);
            _door.transform.position = new Vector3(_door.transform.position.x, _door.transform.position.y, poseZ);
            yield return null;
        }
        if (!_asSwitch)
        {
            SwitchItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box"))
        {
            _boxIsIn = true;
            _box = other.gameObject;
            Debug.Log("est dans le monte charge");
        }
    }
}
