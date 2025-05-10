using TMPro;
using UnityEngine;

public class Prompteur : MonoBehaviour
{
    [SerializeField] private int _hours;
    [SerializeField] private int _minutes;
    [SerializeField] private float _secondes;

    [SerializeField] private int _nombreCache;

    [SerializeField] private TextMeshPro _textMeshPro;

    public bool isShowingTime = true;
    public bool timeIsOver = false;

    private void Update()
    {
        if (!timeIsOver)
        {
            _secondes -= 1 * Time.deltaTime;
            if (_secondes < 0)
            {
                if (_hours == 0 && _minutes == 0)
                {
                    timeIsOver = true;
                }
                else
                {
                    _secondes = 60;
                    _minutes -= 1;
                    if (_minutes <= 0)
                    {
                        _minutes = 60;
                        _hours -= 1;
                    }
                }
            }
        }

        if (isShowingTime)
        {
            _textMeshPro.text = _hours.ToString() + " : " + _minutes.ToString() + " : " + Mathf.RoundToInt(_secondes).ToString();
        }
        else
        {
            _textMeshPro.text = _nombreCache.ToString();
        }
    }
}
