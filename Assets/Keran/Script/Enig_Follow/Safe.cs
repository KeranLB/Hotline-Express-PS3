using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Safe : MonoBehaviour
{
    [SerializeField] private string _targetCode;
    [SerializeField] private Text _text;
    private string _tryCode;
    private int _tryCount;
    [HideInInspector] public bool unlock = false;

    public void test(int value)
    {
        _tryCode += value.ToString();
        _tryCount++;
        _text.text = _tryCode;
        if (_tryCount == 4)
        {
            StartCoroutine(VerifCode());
        }
    }

    IEnumerator VerifCode()
    {
        yield return new WaitForSeconds(1);
        if (_tryCode == _targetCode)
        {
            unlock = true;
        }
        else
        {
            _tryCode = "";
            _tryCount = 0;
            _text.text = _tryCode;
        }
    }
}