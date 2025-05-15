using TMPro;
using UnityEngine;

public class ScreenEngrenage : MonoBehaviour
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private InteractRouage _interactRouage;
    // Update is called once per frame
    void Update()
    {
        float tmp = _interactRouage.currentRotation / _interactRouage._rotationValue;
        _textMeshPro.text = Mathf.RoundToInt(tmp).ToString();
    }
}
