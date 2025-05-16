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
    [SerializeField] private InteractMonteCharge _interactMonteCharge;

    [SerializeField] private float _speedTravel;
    [SerializeField] private GameObject _door;

    private bool _boxIsIn = false;
    [HideInInspector] public bool valide = false;
    private float poseZ;

    [SerializeField] private BoxCollider _boxCollider;

    private bool _isopen;

    private void Start()
    {
        _door.transform.position = _closedPoint.position;
        poseZ = _door.transform.position.z;
    }

    private void Update()
    {
        if (_codeManager.isCorrect && !_isopen)
        {
            _isopen = true;
            StartCoroutine(Travel(_closedPoint, _openPoint, false));
        }
    }

    public void closeDoor()
    {
        if (_boxIsIn && _codeManager.isCorrect)
        {
            valide = true;
            _interactMonteCharge.asFired = true;
            StartCoroutine(Travel(_openPoint, _closedPoint, true));
        }
    }

    private void SwitchItem()
    {
        _box.SetActive(false);
        _rouage.SetActive(true);
        _boxCollider.enabled = false;
        StartCoroutine(Travel(_closedPoint, _openPoint, false));
    }

    IEnumerator Travel(Transform startPoint, Transform endPoint, bool switchItem)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += _speedTravel * Time.deltaTime;
            t = Mathf.Clamp01(t);
            _door.transform.position = Vector2.Lerp(startPoint.position, endPoint.position, t);
            _door.transform.position = new Vector3(_door.transform.position.x, _door.transform.position.y, poseZ);
            yield return null;
        }
        if (switchItem)
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
        }
    }
}
