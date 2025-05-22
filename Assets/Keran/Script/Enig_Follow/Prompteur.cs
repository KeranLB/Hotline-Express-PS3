using System.Collections;
using TMPro;
using UnityEngine;

public class Prompteur : MonoBehaviour
{
    private int _hours = 0;
    private int _minutes = 0;
    private float _secondes = 0;

    [SerializeField] private int _nombreCache;

    public TextMeshPro textMeshPro;

    public bool isShowingTime = true;

    private void Start()
    {
        textMeshPro.text = _hours.ToString() + " : " + _minutes.ToString() + " : " + Mathf.RoundToInt(_secondes).ToString();
        StartCoroutine(Timer());
    }
    private void Update()
    {
        if (!isShowingTime)
        {
            textMeshPro.text = _nombreCache.ToString();
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        _secondes += 1;
        if (_secondes >= 60)
        {
            _secondes = 0;
            _minutes += 1;
            if (_minutes >= 60)
            {
                _minutes = 0;
                _hours += 1;
            }
        }

        if (isShowingTime)
        {
            textMeshPro.text = _hours.ToString() + " : " + _minutes.ToString() + " : " + Mathf.RoundToInt(_secondes).ToString();
        }
        StartCoroutine(Timer());
    }
}
