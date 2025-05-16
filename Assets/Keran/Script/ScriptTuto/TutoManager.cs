using TMPro;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private CodeManager _codeManager;
    [SerializeField] private Controller _controller;
    [SerializeField] private GameObject ecranCodeTuto;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private TextMeshPro _textMeshPro;
   public void Interact()
    {
        if (_codeManager.isCorrect)
        {
            _controller.isLock = false;
            _controller.isInTuto = false;
            _textMeshPro.enabled = false;
            ecranCodeTuto.SetActive(false);
        }
    }
}