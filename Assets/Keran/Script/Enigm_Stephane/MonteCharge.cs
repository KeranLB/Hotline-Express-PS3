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
    [SerializeField] private Transform _door;
    private bool _asSwitch = false;

    private bool _boxIsIn = false;

    private void Update()
    {
        if (_boxIsIn && _codeManager.isCorrect)
        {
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
        float t = 0f;
        while (t < 1f)
        {
            t += _speedTravel * Time.deltaTime;
            t = Mathf.Clamp01(t);
            _door.position = Vector2.Lerp(startPoint.position, endPoint.position, t);
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
        }
    }
}
