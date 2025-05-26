using NUnit.Framework.Internal;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenEngrenage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private InteractRouage _interactRouage;
    [SerializeField] private ObjectClass _objectClass;

    private Dictionary<int, string> _texts = new Dictionary<int, string>();

    private int _correction;
    // Update is called once per frame

    private string _text;

    private void Start()
    {
        _texts.Add(0,"0");
        _texts.Add(324,"1");
        _texts.Add(288,"2");
        _texts.Add(252,"3");
        _texts.Add(216,"4");
        _texts.Add(180,"5");
        _texts.Add(144,"6");
        _texts.Add(108,"7");
        _texts.Add(72,"8");
        _texts.Add(36,"9");
    }
    void Update()
    {
        if (_interactRouage.currentRotation < 0)
        {
            _correction = 360;
        }
        else
        {
            _correction = 0;
        }
        /*
        float tmp = (_interactRouage.currentRotation + _correction) / _interactRouage._rotationValue;        
        _textMeshPro.text = Mathf.RoundToInt(Mathf.Abs(tmp)).ToString();
        //_textMeshPro.text = (360 / (360 - Mathf.RoundToInt(tmp))).ToString();
        */
        if (_texts.TryGetValue(Mathf.RoundToInt((_interactRouage.currentRotation + _correction)), out _text))
        {
            _textMeshPro.text = _text;
        }

        if (_objectClass.interactType == ObjectType.Movable)
        {
            _textMeshPro.enabled = false;
        }
    }
}
