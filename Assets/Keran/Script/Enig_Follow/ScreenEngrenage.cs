using TMPro;
using UnityEngine;

public class ScreenEngrenage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private InteractRouage _interactRouage;

    private int _correction;
    // Update is called once per frame
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
        float tmp = (_interactRouage.currentRotation + _correction) / _interactRouage._rotationValue;


        _textMeshPro.text = Mathf.RoundToInt(tmp).ToString();
    }
}
